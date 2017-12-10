using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchTowerBehaviour : BuildingBaseBehaviour
{
    public float attackRate = 0.5f; //attacks per second
    public float attackRadius = 4f;
    private bool AttackRunning = false;

    private BeeBehaviour target;

    public void Start()
    {
        StartCoroutine(AttackUnits());
        transform.GetChild(0).GetComponent<MeshRenderer>().material.color = owner.color;
    }

    IEnumerator AttackUnits()
    {
        while(true){
            while(!target)
            {
                yield return new WaitForSeconds(0.2f);
                Physics.SphereCast(transform.position, )
            }
            // got target - kill it
            while(target && Vector3.Distance(transform.position, target.transform.position) < 2.5f){
                yield return new WaitForSeconds(1 / attackRate);
                // attack
            }
        }
    }
}
