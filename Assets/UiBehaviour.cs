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

    void Start(){
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
}
