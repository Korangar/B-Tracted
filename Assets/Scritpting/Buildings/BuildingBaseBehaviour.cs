using UnityEngine;

public class BuildingBaseBehaviour : MonoBehaviour {

    public PlayerBehaviour owner;
    public int cost;
    private int healthpoints = 0;
    public int maxHealthpoints = 10;
	public static Vector3 location;
    public event System.Action<BuildingBaseBehaviour> OnDeath;

    public int Healthpoints{
        get{
            return healthpoints;
        }
        set{
            healthpoints = Mathf.Min(maxHealthpoints, value);
            if(healthpoints < 0){
                Death();
            }
        }
    }

	public void SetOwner(PlayerBehaviour owner){
        this.owner = owner;
    }

    public void SetHealthAndMax(int health)
    {
        maxHealthpoints = healthpoints = health;
    }

    public void SetMax(int max){
        maxHealthpoints = max;
    }

    public virtual void Death(){
        StopAllCoroutines();
        if(OnDeath!=null){
            OnDeath(this);
        }
        Destroy(gameObject);
    }

    public virtual void Arrive(BeeBehaviour bee){}

}
