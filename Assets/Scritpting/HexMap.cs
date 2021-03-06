﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour {

	static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt (3) / 2;

	public GameObject hexagon;
	
	public GameObject building;
	public PlayerBehaviour owner;


	public int NumColumns = 20;
	public int NumRows = 15;

	private void OnDrawGizmos() {
		Gizmos.color = Color.black;
		
		for (int i = -50; i <= 50; i++) 
		{
			for (int j = -50; j <= 59; j++) 
			{
                Vector3 pos = Coordinate.Offset2Cube(new Vector2(i, j));
                pos = Coordinate.Cube2Real(pos);
				drawHex(pos);
			}
		}

	}

	private void drawHex(Vector3 center)
	{
		for (int i = 0; i < 6; i++)
		{
			Vector2 p1 = corner(i) * Coordinate.size;
			Vector2 p2 = corner(i + 1) * Coordinate.size;

			Gizmos.DrawLine(new Vector3(p1.x, 0, p1.y) + center, new Vector3(p2.x, 0, p2.y) + center);
		}
	}

	private Vector2 corner(int i)
	{
		i %= 6;
		float angle = 60 * i + 30;
		angle *= Mathf.Deg2Rad;
    	return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
	}
}
