using UnityEngine;
using System;
using System.Collections;

public enum ResourceType
{
    RED,         
    BLUE,
};

public class ResourceBehaviour : BuildingBaseBehaviour {

    public ResourceType type;

    public override void Arrive(BeeBehaviour bee){
        bee.hasPollen=this;
        StartCoroutine(Collecting(bee));
    }

    IEnumerator Collecting(BeeBehaviour bee)
    {   
        bee.transform.GetChild(0).GetComponent<Animator>().SetTrigger("onCollect");
        
        yield return new WaitForSeconds(5.0f);

        bee.transform.GetChild(0).GetComponent<Animator>().SetTrigger("onTakeoff");
        bee.GoTo(bee.owner.headQuarters);
    }
}
