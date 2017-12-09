using UnityEngine;
using System;

public enum ResourceType
{
    RED,         
    BLUE,
};

public class ResourceBehaviour : BuildingBaseBehaviour {

    public ResourceType type;

    public override void Arrive(BeeBehaviour bee){
        bee.hasPollen=this;
        bee.GoTo(owner.headQuarters);
    }
}
