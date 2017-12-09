using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

	#region Variable Declarations
	public GameObject hexagon;

	public int NumColumns = 20;
	public int NumRows = 15;
	public int maxTer = 40;
	public int numOfCapitals = 8;

	private int numberOfTerrainHexes = 0;
	private int nr = 0;

	public Material WaterMaterial;
	public Material LandMaterial;

	private Hexagon[,] hexes;
	private Hexagon[] capitals;
	private List<Hexagon> terrainHexes;
	private Dictionary<Hexagon, GameObject> hexesGO;




	private bool[] terrainHexIsCapital;

	#endregion

	public Hexagon GetHexagonAt(int q, int r){
		return hexes [q, r];
	}

	void Awake () {
		
		DontDestroyOnLoad (this.gameObject);

}
	void Start(){
		GenerateMap ();
	}

	virtual	public void GenerateMap(){
		foreach (Transform child in this.transform) {
			Destroy (child.gameObject);
		}
		numberOfTerrainHexes = 0;
		hexes = new Hexagon[NumColumns, NumRows];
		hexesGO = new Dictionary<Hexagon,GameObject>();
		terrainHexes = new List<Hexagon>();
		capitals = new Hexagon[numOfCapitals];

	
	
	// Map Generation
		for (int i = 0; i < NumColumns; i++) {

			for (int j = 0; j < NumRows; j++) {


				Hexagon hexagonScript = new Hexagon (i, j); 
				hexagonScript.Elevation = -0.5f;
				hexes [i, j] = hexagonScript;
				GameObject hex = (GameObject)Instantiate (hexagon, hexagonScript.Position (), Quaternion.identity);
				hexesGO [hexagonScript] = hex;
			//	hexesGO [hexagonScript].GetComponentInChildren<Hex> ().Hexagon = hexagonScript;
			hex.name = "Hex_" + i + "_" + j;
			// Parent hexGO to Map
			hex.transform.SetParent (this.transform);

			// Create array of HexObjects
			
				hex.GetComponentInChildren<Hex> ().row = j;
				hex.GetComponentInChildren<Hex> ().column = i;

			//Set default material to the Hex, in our case it is water

		}
	}

		UpdateHexagonVisuals();
	//Optimization for Batches
	StaticBatchingUtility.Combine (this.gameObject); 
	
}

	public void UpdateHexagonVisuals(){
		for (int i = 0; i < NumColumns; i++) {
			for (int j = 0; j < NumRows; j++) {
				
				Hexagon h = GetHexagonAt (i, j);
				GameObject hGO = hexesGO [h];

				MeshRenderer mr = hGO.transform.GetComponentInChildren<MeshRenderer> ();
				if (h.Elevation >= 0) {
					mr.material = LandMaterial;
					terrainHexes.Add(h);
					numberOfTerrainHexes++;
					hexesGO [h].GetComponentInChildren<Hex> ().team = 10;
				} else {
					mr.material = WaterMaterial;
				
				}

			}
		}

	
	} 

	public void SetCapitals(){
		terrainHexIsCapital = new bool[numberOfTerrainHexes];
		for (int i = 0; i < numberOfTerrainHexes; i++) {
			terrainHexIsCapital [i] = false;
		}

		//Set Capitals randomly from the terrain hexes
	for (int i = 0; i < numOfCapitals; i++) {
		int j;
		do {	
		j = Random.Range (0, numberOfTerrainHexes);
		} while(terrainHexIsCapital [j]);
		terrainHexIsCapital[j] = true;
		capitals[i] = terrainHexes[j];
		hexesGO[capitals[i]].GetComponentInChildren<SpriteRenderer> ().enabled = true; 
	}



	}

	public Hexagon[] GetHexagonsWithinRadius(Hexagon centerHex, int radius){

		List<Hexagon> hexagonsWithinRadius = new List<Hexagon> ();

		for (int dx = -radius; dx <= radius; dx++) 
		{
			for (int dy = Mathf.Max(-radius, -dx-radius); dy <= Mathf.Min(radius, -dx+radius); dy++) 
			{
				if (centerHex.Q + dx >= 0 && centerHex.R + dy >= 0 && centerHex.Q + dx < NumColumns && centerHex.R + dy < NumRows)
				hexagonsWithinRadius.Add (hexes[centerHex.Q + dx, centerHex.R + dy]);
			

			}

		}
	
		return hexagonsWithinRadius.ToArray ();
	}

	public GameObject GetGOFromHexagon(Hexagon h){
		return hexesGO [h];
	}

	public bool isTerritoryCapital(Hexagon h){
		for (int i = 0; i < numOfCapitals; i++) {
			if (capitals [i] == h) {
				return true;
			}
		}
		return false;
	}

	public void ChangeNrOfTeams(string input){
		Debug.Log (input);
		nr = int.Parse (input);
		if (nr < 8 && nr > 1)
			numOfCapitals = nr;
		Debug.Log (numOfCapitals);
	}
	public void ChangeNrOfAddTer(string input){
		nr = int.Parse (input);
		Debug.Log (nr);
	}
}
