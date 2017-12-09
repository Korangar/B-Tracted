using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class BeeBehaviour : MonoBehaviour
{

    public int HP = 100;

    private NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

	public void GoTo(BuildingBaseBehaviour building)
	{
		agent.SetDestination(building.transform.position);
	}

}
