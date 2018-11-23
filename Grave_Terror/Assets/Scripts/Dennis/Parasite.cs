using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasite : MonoBehaviour
{
	private List<Vector2Int> data = new List<Vector2Int>(5);
	private void OnDestroy()
	{
		foreach (Vector2Int d in data)
		{
			//Do Stuff
		}
	}

	public void Add(int _x, int _y)
	{
		data.Add(new Vector2Int(_x, _y));
	}
}
