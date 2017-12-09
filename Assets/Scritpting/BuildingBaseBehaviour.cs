using UnityEngine;

public class BuildingBaseBehaviour : MonoBehaviour {
	public PlayerBehaviour owner;
	public int healthpoints = 0;
    public int maxHealthpoints = 1000;

    public void TakeDamage()
    {
        healthpoints--;
    }

    public void TakeDamage(int damageAmt)
    {
        healthpoints -= damageAmt;
    }

    public void SetHealth(int health)
    {
        maxHealthpoints = healthpoints = health;
    }

    public virtual void OnDeath(){
        
    }
}
