using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour
{

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
    public List<BarracksBehaviour> barracks = new List<BarracksBehaviour>();
    public Color color;

    public int id;

    private HexMap map;

    public TileBehaviour SelectedTile
    {
        get
        {
            return selectedTile;
        }
        set
        {
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
				StopCoroutine(mapMovement);
				playerMenu.OnMenuOpen(selectedTile);
			}
			else{
				playerMenu.OnMenuClose();
				mapMovement = StartCoroutine(SelectorMovement());
			}
		}
	}

    void Start()
    {
        if (myInput == null)
        {
            myInput = GetComponent<InputBatch>();
        }
        if (playerMenu == null)
        {
            playerMenu = GetComponentInChildren<PlayerMenuManager>();
        }
        StartCoroutine(SelectorMapControll());

        map = FindObjectOfType<HexMap>();
    }

    public void AttackOrder()
    {
        foreach (var e in barracks)
        {
            e.Attack(selectedTile.building);
        }
    }

    public void BuildOrder(BuildingBaseBehaviour b)
    {
        if (!b || resource < b.cost)
        {
            return;
            // TODO if not enough pollen
        }
        resource -= b.cost;
        BuildingBaseBehaviour obj = Instantiate(b, selectedTile.transform.position, Quaternion.identity);
        obj.SetOwner(this);
        selectedTile.building = obj;
    }

    public BeeBehaviour RequestWorker(BuildingBaseBehaviour b)
    {
        BeeBehaviour bee = Instantiate(beePrefab, headQuarters.transform.position, Quaternion.identity);
        bee.SetOwner(this);
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
				else if(playerMenu.CanMenuOpen(selectedTile))
				{
					MenuOpen = true;
				}
			}
			else if(Input.GetButtonDown(myInput.cancel))
			{
				if(menuOpen) MenuOpen = false;
			}
			yield return null;
		}
	}

    private IEnumerator SelectorMovement()
    {
        while (true)
        {
            RaycastHit info;
            if (Physics.Raycast(transform.position, Vector3.down, out info, 1 << LayerMask.NameToLayer("Tiles")))
            {
                selectedTile = info.collider.GetComponent<TileBehaviour>();
                // Debug.DrawLine(selectedTile.transform.position, Vector3.up);
            }
            yield return new WaitForSeconds(moveDelay_s);
            float v_in = 0f;
            float h_in = 0f;
            while (Mathf.Abs(v_in) < deadzone && Mathf.Abs(h_in) < deadzone)
            {
                yield return null;
                v_in = Input.GetAxis(myInput.vertical);
                h_in = Input.GetAxis(myInput.horizontal);
            }
            Vector2 dir = new Vector2(h_in, -v_in);
            dir *= Coordinate.size;

            Vector2 offset = Coordinate.Cube2Offset(Coordinate.RoundReal2Cube(new Vector2(transform.position.x, transform.position.z)) + Coordinate.RoundReal2Cube(dir));

            if (offset.x < 0) offset.x = 0;
            if (offset.y < 0) offset.y = 0;
            if (offset.x >= map.NumColumns) offset.x = map.NumColumns - 1;
            if (offset.y >= map.NumRows) offset.x = map.NumRows - 1;

            transform.position = Coordinate.Offset2Real(offset);
        }
    }
}
