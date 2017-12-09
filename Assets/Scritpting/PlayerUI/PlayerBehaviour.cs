using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour {

	public BeeBehaviour beePrefab;

	public static Vector3 move_v = new Vector3(0.8660254f, 0, 1.5f);
	public static Vector3 move_h = new Vector3(1.732051f, 0, 0);
	public static float moveDelay_s = 0.3f;
	public static float deadzone = 0.9f;
	public HeadQuartersBehaviour headQuarters;
	public int resource = 100;
	[HideInInspector]
	public InputBatch myInput;
	[HideInInspector]
	public PlayerMenuManager playerMenu;
	private Coroutine mapMovement;
	private bool menuOpen = false;
	[HideInInspector]
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

	public bool MenuOpen{
		get{
			return menuOpen;
		}
		set{
			menuOpen = value;
			if(menuOpen){
				playerMenu.OnMenuOpen(selectedTile);
				StopCoroutine(mapMovement);
			}
			else{
				playerMenu.OnMenuClose();
				mapMovement = StartCoroutine(SelectorMovement());
			}
		}
	}

	void Start()
	{
		buildings.Add(headQuarters);
		if(myInput == null){
			myInput = GetComponent<InputBatch>();
		}
		if(playerMenu == null){
			playerMenu = GetComponentInChildren<PlayerMenuManager>();
		}
		StartCoroutine(SelectorMapControll());
	}

	public void AttackOrder()
	{
		foreach(var e in buildings)
		{
			bool isbarr = true;
			if(isbarr)
			{
				// TODO set target for attack
				Debug.Log("Attack "+ selectedTile.name);
			}
		}
	}

	public void BuildOrder(BuildingBaseBehaviour b){
		if(resource < b.cost){
			return;
			// TODO if not enough pollen
		}
		resource -= b.cost;
		BuildingBaseBehaviour obj = 
		Instantiate(b, selectedTile.transform.position, Quaternion.identity);
		obj.SetOwner(this);
		selectedTile.building = obj;
	}

	public BeeBehaviour RequestWorker(BuildingBaseBehaviour b){
		BeeBehaviour bee = Instantiate(beePrefab, headQuarters.transform.position, Quaternion.identity);
		bee.owner = this;
		bee.GoTo(b);
		return bee;
	}

	private IEnumerator SelectorMapControll()
	{
		mapMovement = StartCoroutine(SelectorMovement());

		while(true)
		{
			if(Input.GetButtonDown(myInput.accept))
			{
				if(menuOpen)
				{
					playerMenu.OnSelect();
					MenuOpen = false;
				}
				else
				{
					MenuOpen = true;
				}
			}
			else if(Input.GetButtonDown(myInput.cancel))
			{
				if(menuOpen)
				{
					MenuOpen = false;
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
			if(Physics.Raycast(transform.position, Vector3.down, out info, 1<<LayerMask.NameToLayer("Tiles")))
			{
				selectedTile = info.collider.GetComponent<TileBehaviour>();
				Debug.DrawLine(selectedTile.transform.position, Vector3.up);
			}
			yield return new WaitForSeconds(moveDelay_s);
			float v_in = 0f;
			float h_in = 0f;
			while(Mathf.Abs(v_in) < deadzone && Mathf.Abs(h_in) < deadzone) 
			{
				yield return null;
				v_in = Input.GetAxis(myInput.vertical);
				h_in = Input.GetAxis(myInput.horizontal);
			}
            Vector2 dir = new Vector2(h_in, v_in);
            dir *= Coordinate.size;
            transform.Translate(Coordinate.Cube2Real(Coordinate.RoundReal2Cube(dir)));
        }
	} 
}
