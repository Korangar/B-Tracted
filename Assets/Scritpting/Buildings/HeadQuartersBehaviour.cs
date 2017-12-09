using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadQuartersBehaviour : BuildingBaseBehaviour {
	public void Start(){
		SetHealthAndMax(50);
	}

	public void Update(){
		if (healthpoints <= 0) {
			OnDeath ();
		}
	}

	public override void Arrive(BeeBehaviour bee){
		if(bee.hasPollen!=null){
			owner.resource += 10;
			bee.GoTo(bee.hasPollen);
			bee.hasPollen = null;
		}
	}

}
