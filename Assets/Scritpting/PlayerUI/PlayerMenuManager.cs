using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMenuManager : MonoBehaviour {

	private PlayerBehaviour playerBehaviour;
	public BuildingBaseBehaviour gatherer, barracks, watchtower;
	public GameObject selector_obj;
	[HideInInspector]
	public Text[] entries;
	[HideInInspector]
	public System.Action[] actions;
	private float select_y_offset;
	private int selector = 0;
	private int selectrange_max = 4;
	private static float selectorDelay_s = 0.3f;

	public int Selector{
		get{
			return selector;
		}
		set{
			selector = value;
			Vector3 pos = selector_obj.transform.localPosition;
			pos.y = select_y_offset-value;
			selector_obj.transform.localPosition = pos;
		}
	}


	void Start()
	{
		playerBehaviour = GetComponentInParent<PlayerBehaviour>();
		entries = GetComponentsInChildren<Text>();
		select_y_offset = selector_obj.transform.localPosition.y;
	}

	public void OnMenuOpen(TileBehaviour tb){
		if(tb==null){
			Debug.Log("No Tile!");
		} 
			
		if(tb.building==null){
			//build menu
			Show("Gatherer", "Barracks", "Watchtower");
			actions = new System.Action[]{
				() => playerBehaviour.BuildOrder(gatherer),
				() => playerBehaviour.BuildOrder(barracks),
				() => playerBehaviour.BuildOrder(watchtower)
			};
			
		}
		else if(tb.building.owner == null){
			//resources options
		}
		else if(tb.building.owner == playerBehaviour){
			//my building
			// --> do nothing
		}
		else{
			//enemy building
			// TODO Queue
		}
		
	}

	public void OnMenuClose(){
		Show();
	}

	public void OnSelect(){
		actions[selector]();
	}

	private void Show(params string[] labels){
		Selector = 0;
		selectrange_max = Mathf.Min(labels.Length, entries.Length);
		foreach(var e in entries){
			e.enabled = false;
		}
		for(int i = 0; i< selectrange_max; i++){
			entries[i].enabled = true;
			entries[i].text = labels[i];
		}
		if(selectrange_max>0){
			StartCoroutine("MenuSelectionMovement");
		}
		else{
			StopAllCoroutines();
		}
	}

	public IEnumerator MenuSelectionMovement(){
		while(true)
		{
			yield return new WaitForSeconds(PlayerBehaviour.moveDelay_s);
			// Move Selector
			float v_in = 0f;
			while(Mathf.Abs(v_in) < PlayerBehaviour.deadzone) 
			{
				yield return null;
				v_in = Input.GetAxis(playerBehaviour.myInput.vertical);
			}
			int s = 3 + Selector + (int) Mathf.Sign(v_in);
			Selector = s % selectrange_max;
		}
	}
}
