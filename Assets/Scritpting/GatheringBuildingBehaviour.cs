using System.Collections;
using UnityEngine;

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

    private GameObject[] FindResources(float radius){
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, LayerMask.NameToLayer("Resource"));

        GameObject[] result = new GameObject[colliders.Length];
        for (int i = 0; i < colliders.Length; i++){
            result[i] = colliders[i].gameObject;
        }

        return result;
    }

    public void OnResourceDepleated(){
        
    }

    public override void OnDeath()
    {
        Destroy(gameObject);
    }
}
