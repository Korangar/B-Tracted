using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;

public class BarracksBehaviour : RequestingBaseBuildingBehaviour
{

    TileBehaviour targetTile = null;

    private static float trainDelay = 5f;


    void Start()
    {
        SetHealthAndMax(10);
        SetNumberOfBees(5);
        SetRequestDelay(2f);
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
        if(containsNotTrained(beeList))
        {
            StartCoroutine("TrainBees");
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

    public void TrainBee(Bee bee)
    {
        /**
         * 
         * Circling untrained bee is changed to trained state
         * 
        */
    }

    public void Attack(TileBehaviour tile)
    {
        targetTile = tile;
    }

    public bool containsNotTrained(List<Bee> list){

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
