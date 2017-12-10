using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSounds : MonoBehaviour {
	
	public AudioSource audio;
	public AudioClip[] collectSounds = new AudioClip[2];
	public AudioClip[] attackSounds = new AudioClip[4];
	// Use this for initialization

	public void playCollectSound(){
		audio.clip = collectSounds [Random.Range (0, 2)];
		audio.Play ();
	}

	public void playCAttackSound(){
		audio.clip = attackSounds [Random.Range (0, 4)];
		
		audio.Play ();
	}

}
