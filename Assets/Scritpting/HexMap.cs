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
				GameObject hex = (GameObject)Instantiate (hexagon, Position(i, j), Quaternion.identity);
			//	hexesGO [hexagonScript].GetComponentInChildren<Hex> ().Hexagon = hexagonScript;
				hex.name = i + "_" + j;
			// Parent hexGO to Map
				hex.transform.SetParent (this.transform);
			}
		}
	}
	public Vector3 Position(int Q, int R){
		float radius = 1f;
		float height = radius * 2;
		float width = height * WIDTH_MULTIPLIER;

		float vert = height * 0.75f;
		float horiz = width;
	
		return new Vector3 (
			horiz * (Q + (R & 1) / 2f), 
			0, 
			vert * R
		);
	}
}
