using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    RED,        
    YELLOW,         
    BLUE,

};

public class ResourceBehaviour : BuildingBaseBehaviour {

    public ResourceType type;
    private static int maxHealthpoints = 1000;

    private void Start()
    {
        healthpoints = maxHealthpoints;

        // Setting Layer for GatheringBuilding findResources-method
        gameObject.layer = LayerMask.NameToLayer("Resource");
    }

    public void OnDepleated(){
        
    }
}
