using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowFieldGenerator : MonoBehaviour
{
	public Transform bottomLeft, topRight;
	public float rayHeight;
	//what layers to query when generating the field
	[Tooltip("Only colliders with the FLOOR tag are valid hits")]
	public LayerMask layers;
	//flow field dimensions
	private Vector2Int size;
	//The flow field
	private FField[,] grid;

	public class FField
	{
		public Vector3 direction;
		//0↑  1↗  2→  3↘  4↓  5↙  6←  7↖
		public FField[] neighbours = new FField[8];
	}

	void Start ()
	{
		//--==!!!!!==--Method to be moved to generate field--==!!!!!==--
		//XZ to XY
		size.x = (int)topRight.position.x - (int)bottomLeft.position.x;
		size.y = (int)topRight.position.z - (int)bottomLeft.position.z;

		grid = new FField[size.x, size.y];
		//Preloop
		for (int x = 0; x < size.x; ++x)
		{
			for (int y = 0; y < size.y; ++y)
			{
				grid[x, y] = new FField();
			}

		}
		//Loop to assign neighbours correctly
		for (int x = 0; x < size.x; ++x)
		{
			for (int y = 0; y < size.y; ++y)
			{
				grid[x, y] = new FField();
				//Someones about to get justed
				//0↑
				if(y != 0)
					grid[x, y].neighbours[0] = grid[x, y - 1];
				//1↗
				if(x != size.x - 1 && y != 0)
					grid[x, y].neighbours[1] = grid[x + 1, y - 1];
				//2→
				if(x != size.x - 1)
					grid[x, y].neighbours[1] = grid[x + 1, y];
				//3↘
				if(x != size.x - 1 && y != size.y - 1)
					grid[x, y].neighbours[1] = grid[x + 1, y + 1];
				//4↓
				if(y != size.y - 1)
					grid[x, y].neighbours[1] = grid[x, y + 1];
				//5↙
				if(x != 0 && y != size.y - 1)
					grid[x, y].neighbours[1] = grid[x - 1, y + 1];
				//6←
				if(x != 0)
					grid[x, y].neighbours[1] = grid[x - 1, y];
				//7↖
				if(x != 0 && y != 0)
					grid[x, y].neighbours[1] = grid[x - 1, y - 1];
			}
		}

		//need some way to export this
		//Gotta serialize it
		//GenerateField();
	}

	
	private void GenerateField()
	{
		for (int x = (int)bottomLeft.position.x; x < (int)topRight.position.x; ++x)
		{
			for (int y = (int)bottomLeft.position.z; y < (int)topRight.position.z; ++y)
			{
				//See if we can place a grid segment here
				RaycastHit hit;
				if (Physics.Raycast(new Vector3((float)x + 0.5f, rayHeight, (float)y + 0.5f), Vector3.down, out hit, rayHeight + 1.0f, layers))
				{
					//Valid object hit
					if (hit.transform.gameObject.layer == 8)
					{
						//No action required
						//grid[x, y] = new FField();
						continue;
					}
				}
				//No hit/bad hit
				//as long as you dont look at it, it doesnt look bad
				if (grid[x, y].neighbours[0] != null)
					grid[x, y].neighbours[0].neighbours[4] = null;
				if (grid[x, y].neighbours[1] != null)
					grid[x, y].neighbours[1].neighbours[5] = null;
				if (grid[x, y].neighbours[2] != null)
					grid[x, y].neighbours[2].neighbours[6] = null;
				if (grid[x, y].neighbours[3] != null)
					grid[x, y].neighbours[3].neighbours[7] = null;
				if (grid[x, y].neighbours[4] != null)
					grid[x, y].neighbours[4].neighbours[0] = null;
				if (grid[x, y].neighbours[5] != null)
					grid[x, y].neighbours[5].neighbours[1] = null;
				if (grid[x, y].neighbours[6] != null)
					grid[x, y].neighbours[6].neighbours[2] = null;
				if (grid[x, y].neighbours[7] != null)
					grid[x, y].neighbours[7].neighbours[3] = null;
				//Remove the actual bad hit
				grid[x, y] = null;
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		for (int x = 0; x < size.x; ++x)
		{
			for (int y = 0; x < size.y; ++y)
			{
				if (grid[x, y] != null)
				{
					Gizmos.DrawCube(bottomLeft.position + new Vector3((float)x + 0.5f, 0.05f, (float)y + 0.5f), new Vector3(0.95f, 0.05f, 0.95f));
					Gizmos.DrawLine(bottomLeft.position + new Vector3((float)x + 0.5f, 0.1f, (float)y + 0.5f), bottomLeft.position + new Vector3((float)x + 0.5f, 0.1f, (float)y + 0.5f) + grid[x, y].direction);
				}
			}
		}
	}
}
