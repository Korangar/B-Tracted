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
            t.GenerateMap();
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
                        DestroyImmediate(tile.gameObject);
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
                    if (((HexMap)target).building != null)
                        tile.GetComponent<TileBehaviour>().building = Instantiate(((HexMap)target).building, tile.transform.position, Quaternion.identity).GetComponent<BuildingBaseBehaviour>();
                }

            }
        }
    }
}