using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HexMap))]
public class HexMapInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Make!"))
        {
            HexMap t = (HexMap)target;
            GenerateMap();
        }
    }

    public void GenerateMap()
	{	
		TileBehaviour[] tiles = ((HexMap)target).transform.GetComponentsInChildren<TileBehaviour>();
		for(int i = 1; i < tiles.Length; i++){
			if(tiles[i].building != null)
				DestroyImmediate(tiles[i].building.gameObject);

			DestroyImmediate(tiles[i].gameObject);
		}

		for (int i = 0; i < ((HexMap)target).NumColumns; i++) 
		{
			for (int j = 0; j < ((HexMap)target).NumRows; j++) 
			{
                GameObject hex = (GameObject)Instantiate(((HexMap)target).hexagon, Coordinate.Offset2Real(new Vector2(i, j)), Quaternion.identity);
			//	hexesGO [hexagonScript].GetComponentInChildren<Hex> ().Hexagon = hexagonScript;
				hex.name = i + "_" + j;
			// Parent hexGO to Map
				hex.transform.SetParent (((HexMap)target).transform);
			}
		}
	}

    void OnSceneGUI()
    {
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

        Event e = Event.current;

        if (e.type == EventType.MouseDown)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 hit = ray.GetPoint(distance);

                Vector3 coords = Coordinate.RoundReal2Cube(new Vector2(hit.x, hit.z));
                Vector2 offcoords = Coordinate.Cube2Offset(coords);

                GameObject tile = GameObject.Find(offcoords.x + "_" + offcoords.y);

                if (e.button == 1)
                {
                    if (tile != null)
                    {
                        if (tile.GetComponent<TileBehaviour>().building != null)
                            DestroyImmediate(tile.GetComponent<TileBehaviour>().building.gameObject);

                        DestroyImmediate(tile.gameObject);
                    }
                }

                else if (e.button == 0)
                {
                    if (tile == null)
                    {
                        tile = (GameObject)Instantiate(((HexMap)target).hexagon, Coordinate.Offset2Real(offcoords), Quaternion.identity);
                        //	hexesGO [hexagonScript].GetComponentInChildren<Hex> ().Hexagon = hexagonScript;
                        tile.name = offcoords.x + "_" + offcoords.y;
                        // Parent hexGO to Map
                        tile.transform.SetParent(((HexMap)target).transform);
                    }
                    else
                    {
                        if (tile.GetComponent<TileBehaviour>().building != null)
                        {
                            DestroyImmediate(tile.GetComponent<TileBehaviour>().building.gameObject);
                        }
                    }
                    TileBehaviour tb = tile.GetComponent<TileBehaviour>();

                    if (((HexMap)target).building != null)
                    {
                        tb.building = ((GameObject)PrefabUtility.InstantiatePrefab(((HexMap)target).building)).GetComponent<BuildingBaseBehaviour>();
                        tb.building.transform.position = tile.transform.position;
                        tb.building.SetOwner(((HexMap)target).owner);
                    }
                }

            }
        }
    }
}