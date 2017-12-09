using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour {

	public static Vector3 move_v = new Vector3(0,0,1);
	public static Vector3 move_h = new Vector3(1,0,0);
	public static float moveDelay_s = 0.2f;
	public static float deadzone = 0.9f;


	public int resource = 100;
	private InputBatch myInput; 

	void Start()
	{
		myInput = GetComponent<InputBatch>();
		StartCoroutine(SelectorMovement());
	}

	void Update()
	{
						
		float v_abs = Mathf.Abs(Input.GetAxis(myInput.vertical));
		float h_abs = Mathf.Abs(Input.GetAxis(myInput.horizontal));

		if(v_abs > h_abs)
			{
				float sign = Mathf.Sign(Input.GetAxis(myInput.vertical));
				Debug.DrawRay(transform.position, move_v*sign);
			}
			else
			{
				float sign = Mathf.Sign(Input.GetAxis(myInput.horizontal));
				Debug.DrawRay(transform.position, move_h*sign);
			}
	}

	private IEnumerator SelectorMovement()
	{
		while(true)
		{
			float v_in = 0f;
			float h_in = 0f;
			while(Mathf.Abs(v_in) < deadzone && Mathf.Abs(h_in) < deadzone) 
			{
				yield return null;
				v_in = Input.GetAxis(myInput.vertical);
				h_in = Input.GetAxis(myInput.horizontal);
			}
			
			if(Mathf.Abs(v_in) >= Mathf.Abs(h_in))
			{
				transform.Translate(move_v*Mathf.Sign(v_in));
			}
			else
			{
				transform.Translate(move_h*Mathf.Sign(h_in));
			}
			yield return new WaitForSeconds(moveDelay_s);
		}
	} 

}
