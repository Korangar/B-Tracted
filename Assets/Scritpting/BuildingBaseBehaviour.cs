using UnityEngine;


public class BuildingBaseBehaviour : MonoBehaviour {

    public PlayerBehaviour owner;
    public int healthpoints = 0;
    public int maxHealthpoints = 10;
	public static Vector3 location;

	public void SetOwner(PlayerBehaviour owner){
			this.owner = owner;
		}


    public void TakeDamage()
    {
        healthpoints--;
    }

    public void TakeDamage(int damageAmt)
    {
        healthpoints -= damageAmt;
    }

    public void SetHealthAndMax(int health)
    {
        maxHealthpoints = healthpoints = health;
    }

    public void SetMax(int max){
        maxHealthpoints = max;
    }

    public virtual void OnDeath(){
        StopAllCoroutines();

        Destroy(gameObject);
    }



}
