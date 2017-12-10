using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class BeeBehaviour : MonoBehaviour
{
    public PlayerBehaviour owner;
    public int hp = 100;
    public int DMG = 5;
    public ResourceBehaviour hasPollen = null;
    public BarracksBehaviour isWarrior = null;
    private float arrival_proximity = 0.75f;
    private float arrival_proximity_attack = 0.75f;
    public NavMeshAgent agent;
    public event System.Action<BeeBehaviour> OnDeath;
    private Coroutine movement;

    private int HP{
        get{
            return hp;
        }
        set{
            hp = value;
            if(hp<=0){
                UnitDeath();
            }
        }
    }

	public void GoTo(BuildingBaseBehaviour building)
	{
        if(movement!=null) StopCoroutine(movement);
        if(building.owner && building.owner!=owner)
        {
            movement = StartCoroutine(Attack(building));
        }
        else
        {
            movement = StartCoroutine(WaitForArrival(building));
        }
	}

    private IEnumerator WaitForArrival(BuildingBaseBehaviour building){
        agent.SetDestination(building.transform.position);
        while(building && Vector3.Distance(building.transform.position, transform.position) > arrival_proximity){
            yield return null;
        }
        if(building){
            movement = null;
            building.Arrive(this);
        }
    }

    private IEnumerator Attack(BuildingBaseBehaviour building){
        agent.SetDestination(building.transform.position);
        while(building && Vector3.Distance(building.transform.position, transform.position) > arrival_proximity_attack){
            yield return null;    
        }
        if(!building){
            GoTo(isWarrior);
        }
        else{
            movement = null;
            building.Healthpoints -= (DMG);
            UnitDeath();
        }
    }

    public void UnitDeath(){
        StopAllCoroutines();
        if(OnDeath!=null){
            OnDeath(this);
        }
        Destroy(gameObject);
    }

}
