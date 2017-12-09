using System.Collections;
using UnityEngine;
using System;

public class GatheringBuildingBehaviour : BuildingBaseBehaviour {
    
    private GameObject[] resources;
    private static int maxBees = 5;
    private static float requestDelay = 1f;
    private static float scanRadius = 20f;

    void Start(){
        healthpoints = maxHealthpoints;
        resources = FindResources(scanRadius);
    }

    private void Update()
    {
        if(resources.Length==0){
            OnDeath();
        }
    }

    private IEnumerator RequestBees()
    {
        int i = 0;

        while (i < maxBees)
        {
            i++;
            yield return new WaitForSeconds(requestDelay);
        }
    }

    /**
     * 
     * Get all GameObjects of Resouce-Type in radius, 
     * and subscribe to ResourceDepleted Event
     * 
     */
    private GameObject[] FindResources(float radius){
        // Get all colliders on resource layer in vicinity
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, LayerMask.NameToLayer("Resource"));

        // Get gameobjects of colliderss 
        GameObject[] result = new GameObject[colliders.Length];

        for (int i = 0; i < colliders.Length; i++)
        {
            result[i] = colliders[i].gameObject;

            // Subscribe to ResourceDepleted Event
            ResourceBehaviour resBeh = result[i].GetComponent<ResourceBehaviour>();
            resBeh.ResourceDepleted += new EventHandler(OnResourceDepleated);
        }

        return result;
    }

    public void OnResourceDepleated(object sender, System.EventArgs args){
        resources = FindResources(scanRadius);
    }
}
