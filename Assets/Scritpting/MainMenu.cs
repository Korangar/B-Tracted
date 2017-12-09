using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	private bool PlayerOneReady = false;
	private bool PlayerTwoReady = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Start_1")){
			PlayerOneReady = true;

		}
		if (Input.GetButtonDown("Start_2")){
			PlayerTwoReady = true;
		}
	}
}
