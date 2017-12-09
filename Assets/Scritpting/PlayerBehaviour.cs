using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour {

	public static Vector3 move_v = new Vector3(0.8660254f, 0, 1.5f);
	public static Vector3 move_h = new Vector3(1.732051f, 0, 0);
	public static float moveDelay_s = 0.3f;
	public static float deadzone = 0.9f;
	public int resource = 100;
	public InputBatch myInput;
	public PlayerMenuManager playerMenu;
	public TileBehaviour selectedTile;

	public List<BuildingBaseBehaviour> buildings = new List<BuildingBaseBehaviour>();

	public TileBehaviour SelectedTile{
		get{
			return selectedTile;
		}
		set{
			selectedTile = value;
		}
	}

	void Start()
	{
		if(myInput == null){
			myInput = GetComponent<InputBatch>();
		}
		if(playerMenu == null){
			playerMenu = GetComponentInChildren<PlayerMenuManager>();
		}
		StartCoroutine("SelectorMovement");
		StartCoroutine("SelectorMapControll");
	}

	public void AttackAtSelection(){
		foreach(var e in buildings){
			bool isbarr = true;
			if(isbarr){
				// TODO set target for attack
				Debug.Log("Attack "+ selectedTile.name);
			}
		}
	}

	private IEnumerator SelectorMapControll(){
		bool menuOpen = false;
		while(true){
			if(Input.GetButtonDown(myInput.accept)){
				//TODO select a tile and decide what you can do with it.
				//delegate input to the menu
				if(menuOpen){
					playerMenu.actions[playerMenu.selector]();
				}
				else{
					StopCoroutine("SelectorMovement");
					menuOpen = playerMenu.OnMenuOpen(selectedTile);
				}
			}
			if(Input.GetButtonDown(myInput.cancel)){
				//TODO select a tile and decide what you can do with it.
				//delegate input to the menu
				if(menuOpen){
					playerMenu.StopAllCoroutines();
					menuOpen = false;
					playerMenu.Show();
				}
				
			}
			yield return null;
		}
	}

	private IEnumerator SelectorMovement()
	{
		while(true)
		{
			RaycastHit info;
			if(Physics.Raycast(transform.position, Vector3.down, out info, 0.5f, 1<<9)){
				selectedTile = info.collider.GetComponent<TileBehaviour>();
			}
			
			yield return new WaitForSeconds(moveDelay_s);

			// Move Selector
			float v_in = 0f;
			float h_in = 0f;
			while(Mathf.Abs(v_in) < deadzone && Mathf.Abs(h_in) < deadzone) 
			{
				yield return null;
				v_in = Input.GetAxis(myInput.vertical);
				h_in = Input.GetAxis(myInput.horizontal);
			}

            Vector2 dir = new Vector2(h_in, -v_in);
            dir *= Coordinate.size;

            transform.Translate(Coordinate.Cube2Real(Coordinate.RoundReal2Cube(dir)));
        }
	} 
}
