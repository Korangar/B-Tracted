using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMenuManager : MonoBehaviour {

	private PlayerBehaviour playerBehaviour;

	public BuildingBaseBehaviour gatherer, barracks, watchtower;

	public Text[] entries;
	public System.Action[] actions;
	public int selector = 0;
	public int selectrange_max = 4;

	private static float selectorDelay_s = 0.3f;

	public bool OnMenuOpen(TileBehaviour tb){
		StopAllCoroutines();
		selector = 0;

		if(tb.building==null){
			//build menu
			Show("Gatherer", "Barracks", "Watchtower");
			actions = new System.Action[]{
				() => Instantiate(
					gatherer, 
					playerBehaviour.selectedTile.transform.position, 
					Quaternion.identity),
				() => Instantiate(
					barracks, 
					playerBehaviour.selectedTile.transform.position, 
					Quaternion.identity),
				() => Instantiate(
					watchtower, 
					playerBehaviour.selectedTile.transform.position, 
					Quaternion.identity)
			};
		}
		else if(tb.building.owner == null){
			//resources options
			return false;
		}
		else if(tb.building.owner == playerBehaviour){
			//my building
			// --> do nothing
			return false;
		}
		else{
			//enemy building
			// TODO Queue
		}
		StartCoroutine("MenuSelectionMovement");
		return true;
	}

	private void Show(params string[] labels){
		selectrange_max = Mathf.Min(labels.Length, entries.Length);
		foreach(var e in entries){
			e.enabled = false;
		}
		for(int i = 0; i< selectrange_max; i++){
			entries[i].enabled = true;
			entries[i].text = labels[i];
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
			selector += (int) Mathf.Sign(v_in);
			selector %= selectrange_max;
		}
	}
}
