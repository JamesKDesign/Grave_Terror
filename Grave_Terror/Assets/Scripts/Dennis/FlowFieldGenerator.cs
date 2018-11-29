using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class FFSerializeTranslator
{
	public Vector3 bottomLeft;
	public Vector2Int size;
	public bool[] grid;
}

//The 8 directions possible for the segments
public class Vectoral
{
	public readonly static Vector3[] v = new Vector3[8] {
		Vector3.forward,
		Vector3.Normalize(Vector3.left + Vector3.forward),
		Vector3.left,
		Vector3.Normalize(Vector3.back + Vector3.left),
		Vector3.back,
		Vector3.Normalize(Vector3.right + Vector3.back),
		Vector3.right,
		Vector3.Normalize(Vector3.forward + Vector3.right),
	};
	public readonly static int[] d = new int[8] {
		10,
		14,
		10,
		14,
		10,
		14,
		10,
		14,
	};
}

//Does more then generating but thats its name
public class FlowFieldGenerator : MonoBehaviour
{
	private static FlowFieldGenerator instance;

	public Transform bottomLeft, topRight;
	public float rayHeight;
	[Tooltip("How accurately it checks if the cell is obstructed\nDefault is 0.2")]
	[Range(0.01f,0.5f)]
	public float width = 0.2f;
	//what layers to query when generating the field
	[Tooltip("Only colliders with the FLOOR tag are valid hits")]
	public LayerMask layers;
	//flow field dimensions
	private Vector2Int size;
	//The flow field
	private Segment[,] grid;

	//player Targets
	[Tooltip("The first 2 targets should ALWAYS be the players")]
	public Transform[] targets;
	//Optimization
	private int current = 0;

	private float timer = 0.0f;

	private void Awake()
	{
		instance = this;
	}

	[System.Serializable]
	public class Segment
	{
		public Vector3[] direction = new Vector3[4];
		//0↑  1↗  2→  3↘  4↓  5↙  6←  7↖
		public Segment[] neighbours = new Segment[8];

		public bool queried = false;
		public int distance = 0; //Distance from the objective
	}

	void Start ()
	{
		GenerateField();

		for (int i = 0; i < targets.Length; ++i)
		{
			QueryGrid(targets[i].position, i);
		}

		Debug.Log(targets[0].name + " - " + targets[1].name);
	}

	private void Update()
	{
        timer -= Time.deltaTime;
		if (timer <= 0.0f)
		{
			//Only update them one at a time
			QueryGrid(targets[current].position, current);
			//Change current for the next go
			current++;
			if (current >= targets.Length)
				current = 0;

			timer = 0.1f;
		}
	}

	private void GenerateField()
	{
        Debug.Log("generate field");
		//XZ to XY
		size.x = (int)topRight.position.x - (int)bottomLeft.position.x;
		size.y = (int)topRight.position.z - (int)bottomLeft.position.z;

		grid = new Segment[size.x, size.y];
		//Find valid floor
		for (int x = 0; x < size.x; ++x)
		{
			for (int y = 0; y < size.y; ++y)
			{
				Vector3 point = new Vector3((float)x + 0.5f, rayHeight, (float)y + 0.5f) + bottomLeft.position;
				//See if we can place a grid segment here
				RaycastHit hit;
				//if (Physics.Raycast(point, Vector3.down, out hit, rayHeight + 1.0f, layers))
				if(Physics.BoxCast(point, new Vector3(width, width, width), Vector3.down, out hit, Quaternion.Euler(0,0,0), rayHeight + 1.0f, layers))
				{
					//Valid object hit
					if (hit.transform.gameObject.layer == 8)
					{
						grid[x, y] = new Segment();
						grid[x, y].direction[0] = Vector3.zero;
						continue;
					}
					//Obstruction but it might be destroyed in the future
					else if (hit.transform.gameObject.GetComponent<DestructableObjects>() != null)
					{
						//grid[x, y] = new Segment();
						//grid[x, y].direction[0] = Vector3.zero;
						Parasite para = hit.transform.gameObject.GetComponent<Parasite>();
						//Put some data into the obstruction for when its destryed
						if (para == null)
						{
							para = hit.transform.gameObject.AddComponent<Parasite>();
						}
						//Add data
						para.Add(x, y);
						//continue;
					}
				}
				//No hit/bad hit
				grid[x, y] = null;
			}
		}
		//Loop to assign neighbours correctly
		for (int x = 0; x < size.x; ++x)
		{
			for (int y = 0; y < size.y; ++y)
			{
				if (grid[x, y] == null)
					continue;
				//Someones about to get justed
				//0↑
				if (y != 0)
					grid[x, y].neighbours[0] = grid[x, y - 1];
				//1↗
				if (x != size.x - 1 && y != 0)
					grid[x, y].neighbours[1] = grid[x + 1, y - 1];
				//2→
				if (x != size.x - 1)
					grid[x, y].neighbours[2] = grid[x + 1, y];
				//3↘
				if (x != size.x - 1 && y != size.y - 1)
					grid[x, y].neighbours[3] = grid[x + 1, y + 1];
				//4↓
				if (y != size.y - 1)
					grid[x, y].neighbours[4] = grid[x, y + 1];
				//5↙
				if (x != 0 && y != size.y - 1)
					grid[x, y].neighbours[5] = grid[x - 1, y + 1];
				//6←
				if (x != 0)
					grid[x, y].neighbours[6] = grid[x - 1, y];
				//7↖
				if (x != 0 && y != 0)
					grid[x, y].neighbours[7] = grid[x - 1, y - 1];
			}
		}
	}

	//Set the grid to point towards a target position
	//Should be called everytime the player moves to a different sector
	static public void QueryGrid(Vector3 _target_pos, int _index)
	{
		//Reset the grid
		FlowFieldGenerator.instance.ResetGrid();

		//We're working backwards so get the target position and convert it to segment position
		Vector2Int seg = instance.GetSegmentIndex(_target_pos);
		//Keep the old grid
		if (FlowFieldGenerator.instance.grid[seg.x, seg.y] == null)
			return;

		Queue<Segment> queue = new Queue<Segment>(32);
		queue.Enqueue(FlowFieldGenerator.instance.grid[seg.x, seg.y]);
		//Mark it as checked
		FlowFieldGenerator.instance.grid[seg.x, seg.y].queried = true;
		FlowFieldGenerator.instance.grid[seg.x, seg.y].distance = 0;

		Segment segment = null;
		Segment neighbour = null;

		//While the queue has stuff in it
		while (queue.Count != 0)
		{
			segment = queue.Dequeue();

			for(int s = 0; s < 8; ++s)
			{
				neighbour = segment.neighbours[s];
				//It doesn't exist
				if (neighbour == null)
					continue;
				//We've already parsed this segment
				if (neighbour.queried)
				{
					//Just check if its closer
					if (segment.distance + Vectoral.d[s] < neighbour.distance)
					{
						neighbour.direction[_index] = Vectoral.v[s];
						neighbour.distance = segment.distance + Vectoral.d[s];
					}
					continue;
				}

				//Voodoo arrays because only 8 directions are possible
				neighbour.direction[_index] = Vectoral.v[s];

				//Mark it as queried so we dont calculate it again
				neighbour.queried = true;
				neighbour.distance = segment.distance + Vectoral.d[s];
				//Put this neighbour on the queue
				queue.Enqueue(neighbour);
			}
		}
	}

	//Find out which segment in the grid we're standing on and get where we need to go
	static public Vector3 GetDirectionFromGrid(Vector3 _position, int _index)
	{
		//Convert to segment
		Vector2Int seg = instance.GetSegmentIndex(_position);
		//Return the segment's direction
		return FlowFieldGenerator.instance.grid[seg.x, seg.y].direction[_index];

		//return Vector3.zero;
	}

	//Convert a position in world space to a segment of the grid
	public Vector2Int GetSegmentIndex(Vector3 _position)
	{
		//XZ to XY
		Vector2Int seg = new Vector2Int();
		//convert world position to grid positon
		seg.x = (int)((FlowFieldGenerator.instance.bottomLeft.position.x * -1.0f) + _position.x);
		seg.y = (int)((FlowFieldGenerator.instance.bottomLeft.position.z * -1.0f) + _position.z);

		return seg;
	}

	public Segment GetSegment(Vector3 _position)
	{
		//XZ to XY
		Vector2Int seg = new Vector2Int();
		//convert world position to grid positon
		seg.x = (int)((FlowFieldGenerator.instance.bottomLeft.position.x * -1.0f) + _position.x);
		seg.y = (int)((FlowFieldGenerator.instance.bottomLeft.position.z * -1.0f) + _position.z);

		return grid[seg.x, seg.y];
	}

	public Vector3 GetSegmentDirection(Vector3 _position, int _index)
	{
		//XZ to XY
		Vector2Int seg = new Vector2Int();
		//convert world position to grid positon
		seg.x = (int)((bottomLeft.position.x * -1.0f) + _position.x);
		seg.y = (int)((bottomLeft.position.z * -1.0f) + _position.z);

		//Auto reject
		if (seg.x >= size.x || seg.y >= size.y ||
			seg.x < 0 || seg.y < 0)
			return Vector3.zero;

		if (grid[seg.x, seg.y] != null)
			return grid[seg.x, seg.y].direction[_index];
		else
			return Vector3.zero;
	}

	//Does exactly whats on the label
	void ResetGrid()
	{
		for (int x = 0; x < size.x; ++x)
		{
			for (int y = 0; y < size.y; ++y)
			{
				//Reset the queried and distance variable
				if (grid[x, y] != null)
				{
					grid[x, y].queried = false;
					grid[x, y].distance = 0;
				}
			}
		}
	}

	public int AttemptTarget(int _index)
	{
		if (targets[_index].GetComponent<PlayerHealth>().currentHealth > 0.0f)
		{
			return _index;
		}
		return -1;
	}

	//For parasites the where put on destructible objects during grid creation
	//Recalculates if the tile is still valid (or if an invalid tile is now valid)
	public void Regenerate(Vector2Int _data)
	{
		//return; //void everything for now
		//Dont allow data thats not in the grid
		if (_data.x >= size.x || _data.y >= size.y ||
			_data.x < 0 || _data.y < 0)
			return;

		//Dont do anything if its already existing
		if (grid[_data.x, _data.y] != null)
			return;

		//Set segment
		grid[_data.x, _data.y] = new Segment();
		Segment seg = grid[_data.x, _data.y];

		//0↑
		if (_data.y != 0)
		{
			seg.neighbours[0] = grid[_data.x, _data.y - 1];
			//and back
			if(grid[_data.x, _data.y - 1] != null)
				grid[_data.x, _data.y - 1].neighbours[4] = seg;

		}
		//1↗
		if (_data.x != size.x - 1 && _data.y != 0)
		{
			seg.neighbours[1] = grid[_data.x + 1, _data.y - 1];
			//and back
			if(grid[_data.x + 1, _data.y - 1] != null)
				grid[_data.x + 1, _data.y - 1].neighbours[5] = seg;
		}
		//2→
		if (_data.x != size.x - 1)
		{
			seg.neighbours[2] = grid[_data.x + 1, _data.y];
			//and back
			if(grid[_data.x + 1, _data.y] != null)
				grid[_data.x + 1, _data.y].neighbours[6] = seg;
		}
		//3↘
		if (_data.x != size.x - 1 && _data.y != size.y - 1)
		{
			seg.neighbours[3] = grid[_data.x + 1, _data.y + 1];
			//and back
			if(grid[_data.x + 1, _data.y + 1] != null)
				grid[_data.x + 1, _data.y + 1].neighbours[7] = seg;
		}
		//4↓
		if (_data.y != size.y - 1)
		{
			seg.neighbours[4] = grid[_data.x, _data.y + 1];
			//and back
			if(grid[_data.x, _data.y + 1] != null)
				grid[_data.x, _data.y + 1].neighbours[0] = seg;
		}
		//5↙
		if (_data.x != 0 && _data.y != size.y - 1)
		{
			seg.neighbours[5] = grid[_data.x - 1, _data.y + 1];
			//and back
			if(grid[_data.x - 1, _data.y + 1] != null)
				grid[_data.x - 1, _data.y + 1].neighbours[1] = seg;
		}
		//6←
		if (_data.x != 0)
		{
			seg.neighbours[6] = grid[_data.x - 1, _data.y];
			//and back
			if(grid[_data.x - 1, _data.y] != null)
				grid[_data.x - 1, _data.y].neighbours[2] = seg;
		}
		//7↖
		if (_data.x != 0 && _data.y != 0)
		{
			seg.neighbours[7] = grid[_data.x - 1, _data.y - 1];
			//and back
			if(grid[_data.x - 1, _data.y - 1] != null)
				grid[_data.x - 1, _data.y - 1].neighbours[3] = seg;
		}
	}

	public static FlowFieldGenerator GetInstance()
	{
		return instance;
	}

	private void OnDrawGizmosSelected()
	{
		for (int x = 0; x < size.x; ++x)
		{
			for (int y = 0; y < size.y; ++y)
			{
				if (grid[x, y] != null)
				{
					Gizmos.DrawWireCube(bottomLeft.position + new Vector3((float)x + 0.5f, 0.05f, (float)y + 0.5f), new Vector3(0.95f, 0.05f, 0.95f));
					Debug.DrawRay(bottomLeft.position + new Vector3((float)x + 0.5f, 0.1f, (float)y + 0.5f), grid[x, y].direction[0] * 0.5f, Color.green);
					Debug.DrawRay(bottomLeft.position + new Vector3((float)x + 0.5f, 0.1f, (float)y + 0.5f), grid[x, y].direction[1] * 0.5f, Color.blue);
					//Debug.DrawRay(bottomLeft.position + new Vector3((float)x + 0.5f, 0.1f, (float)y + 0.5f), Vector3.up * grid[x, y].distance, Color.red);
				}
			}
		}
	}
}
