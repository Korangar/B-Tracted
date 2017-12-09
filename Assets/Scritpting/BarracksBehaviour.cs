using System.Collections;
using UnityEngine;

public class BarracksBehaviour : BuildingBaseBehaviour {

    TileBehaviour targetTile;

    private static int maxBees = 10;
    private static float requestDelay = 1f;
    private static float trainDelay = 5f; 

    void Start(){
        // StartCoroutine RequestBees
    }

    private void Update()
    {
        // if request bees finished, and not training
            // StartCoroutine TrainBees
    }

    private IEnumerator RequestBees(){
        int i = 0;

        while(i < maxBees){
            // REQUEST BEES
            i++;
            yield return new WaitForSeconds(requestDelay);
        }
    }

    private IEnumerator TrainBees(){
        int i = 0;
        int bees = 0 /*number of current bees here*/; 

        while(i < bees){
            //trainBeeAtIndex(i);
            i++;
            yield return new WaitForSeconds(trainDelay);
        }
    }

    public void Attack(TileBehaviour tile){
        targetTile = tile;   
    }

    public void OnDefeat(){
        
    }
}
