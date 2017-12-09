using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BarracksBehaviour : BuildingBaseBehaviour
{

    TileBehaviour targetTile = null;

    private static int maxBees = 10;
    private static float requestDelay = 1f;
    private static float trainDelay = 5f;
    private List<Object> beeList = new List<Object>();

    void Start()
    {
        StartCoroutine("RequestBees");
    }

    void Update()
    {
        if (healthpoints <= 0)
            OnDeath();
        if (beeList.Count < 10)
        {
            StartCoroutine("RequestBees");
        }
        if(containsNotTrained(beeList)){
            StartCoroutine("TrainBees");
        }
    }
    private IEnumerator RequestBees()
    {
        int i = 0;

        while (beeList.Count < maxBees)
        {
            beeList.Add(RequestBee());
            i++;
            yield return new WaitForSeconds(requestDelay);
        }
    }

    private IEnumerator TrainBees()
    {
        int i = 0;

        while (i < beeList.Count)
        {
            if (i == 0 /*!beeList.ElementAt(i).isTrained()*/)
            {
                TrainBee(beeList.ElementAt(i));
                i++;
                yield return new WaitForSeconds(trainDelay);
            }
            else
            {
                i++;
                continue;
            }
        }
    }

    public Object RequestBee()
    {
        /*
         * Request bee and return refererence
         */
        return null;
    }

    public void TrainBee(Object bee)
    {
        /**
         * 
         * Circling untrained bee is changed to trained state
         * 
        */
    }

    public void OnBeeDeath(object sender, System.EventArgs args)
    {
        StartCoroutine("RequestBees");
    }

    public void Attack(TileBehaviour tile)
    {
        targetTile = tile;
    }

    public bool containsNotTrained(List<Object> list){

        for (int i = 0; i < list.Count; i++){
            if( false
               // !list.ElementAt(i).Trained
            ){
                return true;
            }
        }

        return false;
    }
}
