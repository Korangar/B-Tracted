using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStash : BuildingBaseBehaviour {
	 
	public GameObject unit;
	public List<GameObject> currentUnits = new List<GameObject>();

	public void Start(){
		SetHealthAndMax(10);
	}

	public void Update(){
		if (healthpoints <= 0) {
			OnDeath ();
		}
	}

	public void createUnit(){
		currentUnits.Add ((GameObject)GameObject.Instantiate (unit, location, Quaternion.identity));
	}

}
