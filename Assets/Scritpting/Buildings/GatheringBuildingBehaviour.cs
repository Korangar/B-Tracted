using System.Collections.Generic;
using UnityEngine;
using System;

public class GatheringBuildingBehaviour : BuildingBaseBehaviour {
    
    private ResourceBehaviour[] resources;
    private BeeBehaviour[] bees;
   
    private static float scanRadius = 5f;

    void Start(){
        SetHealthAndMax(10);
        FindResources(scanRadius);
    }

    private void HireBee(int i){
        BeeBehaviour temp = owner.RequestWorker(resources[i]);
        temp.OnDeath += OnWorkerDeath;
    }

    private void FindResources(float radius){
        // Get all colliders on resource layer in vicinity
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, 1<<LayerMask.NameToLayer("Resources"));
        Debug.Log(colliders.Length);
        Debug.DrawRay(transform.position, Vector3.right*radius, Color.red, 20f);
        // Get gameobjects of colliderss 
        resources = new ResourceBehaviour[colliders.Length];
        bees = new BeeBehaviour[colliders.Length];

        for (int i = 0; i < colliders.Length; i++)
        {
            resources[i] = colliders[i].gameObject.GetComponent<ResourceBehaviour>();
            // Subscribe to ResourceDepleted Event
            if(bees[i]==null)
            {
                HireBee(i);
            }
        }
    }

    public void OnWorkerDeath(BeeBehaviour bee)
    {
        for (int i = 0; i < bees.Length; i++)
        {
            if(bees[i]==bee)
            {
                HireBee(i);
            }
            
        }
    }
}
