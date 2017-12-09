using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour {

	static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt (3) / 2;

	public GameObject hexagon;


	public int NumColumns = 20;
	public int NumRows = 15;

	public void GenerateMap()
	{	
		TileBehaviour[] tiles = transform.GetComponentsInChildren<TileBehaviour>();
		for(int i = 1; i < tiles.Length; i++){
			DestroyImmediate(tiles[i].gameObject);
		}

		for (int i = 0; i < NumColumns; i++) 
		{
			for (int j = 0; j < NumRows; j++) 
			{
                Vector3 pos = Coordinate.Offset2Cube(new Vector2(i, j));
                pos = Coordinate.Cube2Real(pos);

                GameObject hex = (GameObject)Instantiate (hexagon, pos, Quaternion.identity);
			//	hexesGO [hexagonScript].GetComponentInChildren<Hex> ().Hexagon = hexagonScript;
				hex.name = i + "_" + j;
			// Parent hexGO to Map
				hex.transform.SetParent (this.transform);
			}
		}
	}
}
