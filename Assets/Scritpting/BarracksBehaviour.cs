using System.Collections;
using UnityEngine;

public class BarracksBehaviour : BuildingBaseBehaviour {

    TileBehaviour targetTile = null;

    private static int maxBees = 10;
    private static float requestDelay = 1f;
    private static float trainDelay = 5f;

    void Start(){
    }

    private void Update()
    {
        if (healthpoints <= 0)
            OnDeath();
        
    }

    private IEnumerator RequestBees(){
        int i = 0;

        while(i < maxBees){
            RequestBee();
            i++;
            yield return new WaitForSeconds(requestDelay);
        }
    }

    private IEnumerator TrainBees(){
        int i = 0;
        int bees = 0; 

        while(i < bees){
            TrainBee();
            i++;
            yield return new WaitForSeconds(trainDelay);
        }
    }

    public void RequestBee(){
        // Request Bee here
    }

    public void TrainBee(){
        /**
         * 
         * Circling untrained bee is changed to trained state
         * 
        */
    }

    public void OnBeeDeath(){
       // Request new bee
    }

    public void OnBeeTrained(){
        // Spawn trained bee
    }

    public void Attack(TileBehaviour tile){
        targetTile = tile;   
    }

    public override void OnDeath(){
        
    }
}
