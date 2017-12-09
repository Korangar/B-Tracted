using UnityEngine;

public enum ResourceType
{
    //Placeholdertypes
    RED,        
    YELLOW,         
    BLUE,

};

public class ResourceBehaviour : BuildingBaseBehaviour {

    public ResourceType type;

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Resource");
    }

    public void depleatResource()
    {
        TakeDamage();
    }

    public void delpeatResource(int depleted)
    {
        TakeDamage(depleted);
    }

    public override void OnDeath()
    {
        Destroy(gameObject);
    }
}
