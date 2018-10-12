using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowFieldGenerator : MonoBehaviour
{
	public Transform bottomLeft, topRight;
	public float rayHeight;
	//what layers to query when generating the field
	public LayerMask layers;
	//flow field dimensions
	private Vector2Int size;
	//The flow field
	private FField[,] grid;

	public class FField
	{
		public bool valid = false;
		public Vector3 direction;
		//0↑  1↗  2→  3↘  4↓  5↙  6←  7↖
		public FField[] neighbours = new FField[8];
	}

	void Start ()
	{
		//XZ to XY
		size.x = (int)topRight.position.x - (int)bottomLeft.position.x;
		size.y = (int)topRight.position.z - (int)bottomLeft.position.z;

		grid = new FField[size.x, size.y];
		//Setup the neighbours array for each field
		for (int x = 0; x < size.x; ++x)
		{
			for (int y = 0; y < size.y; ++y)
			{
				grid[x, y] = new FField();
				//Someones about to get justed
				//0↑
				grid[x, y].neighbours[0] = grid[x, y - 1];
				//1↗
				grid[x, y].neighbours[1] = grid[x + 1, y - 1];
				//2→
				grid[x, y].neighbours[1] = grid[x + 1, y];
				//3↘
				grid[x, y].neighbours[1] = grid[x + 1, y + 1];
				//4↓
				grid[x, y].neighbours[1] = grid[x, y + 1];
				//5↙
				grid[x, y].neighbours[1] = grid[x - 1, y + 1];
				//6←
				grid[x, y].neighbours[1] = grid[x - 1, y];
				//7↖
				grid[x, y].neighbours[1] = grid[x - 1, y - 1];
			}
		}
	}

	
	private void GenerateField()
	{
		for (int x = (int)bottomLeft.position.x; x < (int)topRight.position.x; ++x)
		{
			for (int y = (int)bottomLeft.position.z; y < (int)topRight.position.z; ++y)
			{
				//See if we can place a grid segment here
				RaycastHit hit;
				if (Physics.Raycast(new Vector3(x, rayHeight, y), Vector3.down, out hit, rayHeight + 1.0f, layers))
				{
					//Valid object hit
					if (hit.transform.gameObject.layer == 8)
					{
						grid[x, y].valid = true;
						continue;
					}
				}
				//No hit/bad hit, purge field
				grid[x, y] = null;
			}
		}
	}
}
