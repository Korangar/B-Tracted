﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadQuartersBehaviour : BuildingBaseBehaviour {
	public void Start(){		
		transform.GetChild(0).GetComponent<MeshRenderer>().material.color = owner.color;

		OnDeath += GameOver;
	}

	public override void Arrive(BeeBehaviour bee){
		if(bee.hasPollen!=null){
			owner.resource += 10;
			bee.GoTo(bee.hasPollen);
			bee.hasPollen = null;
		}
	}

	public void GameOver(BuildingBaseBehaviour building)
	{
		if(building.owner.id != 0)
		{
			SceneManager.LoadScene("RedVictoryScene");
		} else {
			SceneManager.LoadScene("BlueVictoryScene");
		}
	}

}
