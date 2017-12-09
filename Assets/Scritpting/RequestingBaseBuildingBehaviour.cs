using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography.X509Certificates;

public class RequestingBaseBuildingBehaviour : BuildingBaseBehaviour
    {
    public int nmbrOfBees;
    public float requestDelay;
    public List<Bee> beeList = new List<Bee>();

    private IEnumerator RequestBees()
    {
        int i = 0;

        while (beeList.Count < nmbrOfBees)
        {
            beeList.Add(RequestBee());
            i++;
            yield return new WaitForSeconds(requestDelay);
        }
    }

    public Bee RequestBee()
    {
        /*
         * Request bee and return refererence
         */
        return null;
    }

    public void OnBeeDeath(object sender, System.EventArgs args)
    {
        StartCoroutine("RequestBees");
    }

    public void SetNumberOfBees(int bees){
        nmbrOfBees = bees;
    }

    public void SetRequestDelay(float requestDelay){
        this.requestDelay = requestDelay;
    }
    }

