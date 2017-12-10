using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchTowerBehaviour : BuildingBaseBehaviour
{
    public int dmg = 7;
    public float attackRate = 0.5f; //attacks per second
    public float attackRadius = 5f;
    private bool AttackRunning = false;

    public void Start()
    {
        StartCoroutine(AttackUnits());
        transform.GetChild(0).GetComponent<MeshRenderer>().material.color = owner.color;
    }

    private BeeBehaviour GetNextEnemy(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius, 1<<LayerMask.NameToLayer("Units"));
        foreach(Collider c in colliders){
            BeeBehaviour bee = c.GetComponent<BeeBehaviour>();
            if(bee.owner != owner){
                return bee;
            }
        }
        return null;
    }

    public IEnumerator AttackUnits(){
        BeeBehaviour target= null;
        while(true){
            while(!target){
                target = GetNextEnemy();
                yield return new WaitForSeconds(0.1f);
            }
            while(target){
                if(Vector3.Distance(transform.position, target.transform.position)>attackRadius){
                    target = null;
                    break;
                }
                else{
                    target.HP -= dmg;
                    Debug.DrawLine(transform.position + Vector3.up * 1.0f, target.transform.GetChild(0).position, Color.black, 0.5f);
                    StartCoroutine(DrawShotEffect(target));
                    yield return new WaitForSeconds(1/attackRate);
                }
            }
        }
    }

    private IEnumerator DrawShotEffect(BeeBehaviour target){
        LineRenderer line = GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position+Vector3.up);
        line.SetPosition(1, target.transform.position);
        line.enabled = true;
        yield return new WaitForSeconds(0.4f);
        line.enabled = false;
    }

}