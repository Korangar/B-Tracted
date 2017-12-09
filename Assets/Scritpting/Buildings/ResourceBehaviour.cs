using UnityEngine;
using System;

public enum ResourceType
{
    RED,         
    BLUE,
};

public class ResourceBehaviour : BuildingBaseBehaviour {

    public ResourceType type;
    public event EventHandler ResourceDepleted;

    private void Start()
    {
        SetLayerMask("Resource");
    }

    public void DepleatResource()
    {
        TakeDamage();
    }

    public void DepleatResource(int depleted)
    {
        TakeDamage(depleted);
    }

    public void OnResourceDepleted()
    {
        EventHandler handler = ResourceDepleted;
        if(handler != null){
            if(healthpoints <= 0){
                handler(this, EventArgs.Empty);
            }
        }
    }

    private void SetLayerMask(String mask){
        gameObject.layer = LayerMask.NameToLayer(mask);
    }

    public override void Arrive(BeeBehaviour bee){
        bee.hasPollen=this;
        bee.GoTo(owner.headQuarters);
    }
}
