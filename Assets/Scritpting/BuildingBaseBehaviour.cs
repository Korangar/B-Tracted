using UnityEngine;

<<<<<<< HEAD
public class BuildingBaseBehaviour : MonoBehaviour {
	public PlayerBehaviour owner;
	public int healthpoints = 0;
	public static Vector3 location;
	public static void requestUnit(){
//		owner.sendUnit (location);
	}
=======
public class BuildingBaseBehaviour : MonoBehaviour
{
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
>>>>>>> 007a5472908fc5c7f41c59bfd668336998eb56f9
}
