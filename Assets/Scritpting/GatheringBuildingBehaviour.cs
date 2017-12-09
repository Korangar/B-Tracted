using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography.X509Certificates;

public class GatheringBuildingBehaviour : BuildingBaseBehaviour {
    
    private GameObject[] resources;
    private static int maxHealthpoints = 1000;
    private static int maxBees = 5;
    private static float requestDelay = 1f;
    private static float scanRadius = 20f;

    void Start(){
        healthpoints = maxHealthpoints;
        resources = FindResources(scanRadius);
    }

    private IEnumerator RequestBees()
    {
        int i = 0;

        while (i < maxBees)
        {
            // REQUEST BEES
            i++;
            yield return new WaitForSeconds(requestDelay);
        }
    }

    private GameObject[] FindResources(float radius){
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, LayerMask.NameToLayer("Resource"));

        GameObject[] result = new GameObject[colliders.Length];
        for (int i = 0; i < colliders.Length; i++){
            result[i] = colliders[i].gameObject;
        }

        return result;
    }

    public void OnDefeat(){
        
    }
}
