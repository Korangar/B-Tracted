using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class UiBehaviour : MonoBehaviour {

    public PlayerBehaviour playerOne;
    public PlayerBehaviour playerTwo;

    private Text playerOneText;
    private Text playerTwoText;
    private GameObject tipTwo;

    void Start(){
        PlayerBehaviour[] players = FindObjectsOfType<PlayerBehaviour>();
        foreach(PlayerBehaviour p in players){
            if(p.id == 0)
            {
                playerOne = playerOne == null ? p : playerOne;
            }
            else if(p.id == 1)
            {
                playerTwo = playerTwo == null ? p : playerTwo;
            }
        }

        if (playerOne != null && playerTwo != null)
        {
            Text[] texts = gameObject.GetComponentsInChildren<Text>();

            foreach (Text text in texts){
                if(text.name == "Player1_Text") {
                    playerOneText = text;
                }
                if(text.name == "Player2_Text") {
                    playerTwoText = text;
                }
            }
            tipTwo = GameObject.Find("tipTwo");
            StartCoroutine(VanishTip());
            StopCoroutine(KeepMeUpdated());
        }
        else{
            Debug.Log("UI has been corrupted because 1 or more players couldn't be found. " +
                      "Make sure all id are set in PlayerBehaviours.");
        }

    }

    public void updateStats(PlayerBehaviour player){
        if(player == playerOne){
            playerOneText.text = player.resource + "";
        }
        if (player == playerTwo)
        {
            playerTwoText.text = player.resource + "";
        }
    }

    IEnumerator KeepMeUpdated(){
        while(true){
            updateStats(playerOne);
            updateStats(playerTwo);
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator VanishTip(){
        if (tipTwo != null)
        {
            yield return new WaitForSeconds(10f);
            Destroy(tipTwo);
        }
    }

}
