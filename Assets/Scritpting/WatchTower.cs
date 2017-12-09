using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchTower : BuildingBaseBehaviour {

	public void Start(){
			SetHealthAndMax(10);
	}

	public void Update(){
		if (healthpoints <= 0) {
			OnDeath ();
		}
	}

	public void OnCollisionEnter(Collision collision){
		foreach (ContactPoint contact in collision.contacts)
		{
			Debug.DrawRay(contact.point, contact.normal, Color.red);
			//TODO Add Collider to the object
		}
	
	
	}


}
