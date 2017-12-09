using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStash : BuildingBaseBehaviour {
	 
	public GameObject unit;
	public List<GameObject> currentUnits = new List<GameObject>();

	public void Start(){
		SetHealthAndMax(10);
		transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].color = owner.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
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
