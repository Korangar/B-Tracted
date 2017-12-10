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
		if (Input.GetAxis("Accept_1") != 0){
			playerOneReady = true;
			playerOne.GetComponent<CanvasGroup>().alpha = 1;

		}
		if (Input.GetAxis("Accept_2") != 0){
			playerTwoReady = true;
			playerTwo.GetComponent<CanvasGroup> ().alpha = 1;
		}

		if (playerOneReady && playerTwoReady) {
			SceneManager.LoadScene("BTracted");
		}
	}
}
