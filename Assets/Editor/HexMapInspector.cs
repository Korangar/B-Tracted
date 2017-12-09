using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HexMap))]
public class HexMapInspector : Editor  {
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if(GUILayout.Button("Make!")){
			HexMap t = (HexMap)target;
			t.GenerateMap();
		}
	}
}
