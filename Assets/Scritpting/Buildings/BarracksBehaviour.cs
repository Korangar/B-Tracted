using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;

public class BarracksBehaviour : BuildingBaseBehaviour
{
    private static float restockDelay_s = 1.5f;
    private static float trainDelay_s = 5f;
    public int max_recruits = 5;
    private List<BeeBehaviour> bees = new List<BeeBehaviour>();

    void Start()
    {
        owner.barracks.Add(this);
        OnDeath += (e) => owner.barracks.Remove(this);
        StartCoroutine(Restock());
        transform.GetChild(0).GetComponent<MeshRenderer>().material.color = owner.color;
    }

    public void Attack(BuildingBaseBehaviour building)
    {
        foreach(var b in bees){
            if(b.isWarrior){
                b.GoTo(building);
            }
        }
    }

    public override void Arrive(BeeBehaviour bee){
        if(!bee.isWarrior){
            StartCoroutine(Train(bee));
        } 
    }

    private IEnumerator Restock(){
        while(true){
            while(bees.Count<max_recruits){
                yield return new WaitForSeconds(restockDelay_s);
                bees.Add(HireBee());
            }
            yield return null;
        }
    }

    private IEnumerator Train(BeeBehaviour bee){
        yield return new WaitForSeconds(trainDelay_s);
        bee.isWarrior = this;
    }

    private BeeBehaviour HireBee(){
        BeeBehaviour temp = owner.RequestWorker(this);
        temp.OnDeath += OnDeathOfAWarrior;
        return temp;
    }

    private void OnDeathOfAWarrior(BeeBehaviour bee){
        bees.Remove(bee);
    }
}
