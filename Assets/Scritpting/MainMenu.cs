using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	private bool playerOneReady = false;
	private bool playerTwoReady = false;
	public Image playerOne;
	public Image playerTwo;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Start_1")){
			playerOneReady = true;
			playerOne.enabled = true;

		}
		if (Input.GetButtonDown("Start_2")){
			playerTwoReady = true;
			playerOne.enabled = true;
		}

		if (playerOneReady && playerTwoReady) {
			SceneManager.LoadScene("BTracted");
		}
	}
}
