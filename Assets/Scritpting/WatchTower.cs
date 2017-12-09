using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchTower : BuildingBaseBehaviour {

	public List<GameObject> unitsWithinRange = new List<GameObject>(); 
	public double attackRate = 0.5; //attacks per second
	private bool AttackRunning = false;

	public void Start(){
			SetHealthAndMax(10);
	}

	public void Update(){
		if (healthpoints <= 0) {
			OnDeath ();
			StopAllCoroutines ();
		}
	}

	public void OnCollisionStay(Collision collision){
		if (!unitsWithinRange.Contains (collision.gameObject)) {
			unitsWithinRange.Add (collision.gameObject);
			if (!AttackRunning) {
				StartCoroutine ("AttackUnits");
			}
		}
	}

	IEnumerator AttackUnits(){
		AttackRunning = true;
		while (unitsWithinRange.Count > 0) {
//			unitsWithinRange [0].GetComponentInChildren<unitBehaviour> ().decHP ();   //Decrease Unit's HP
			unitsWithinRange.RemoveAt(0);
			yield return new WaitForSeconds (1 / attackRate);
		}
		AttackRunning = false;
	}


}
