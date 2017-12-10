using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioSource audio = new AudioSource();
	public AudioClip[] songs = new AudioClip[6];
	public int numSong = 0;
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (this.gameObject);
		audio.clip = songs [numSong] as AudioClip;
		audio.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!audio.isPlaying) {
			numSong++;
			audio.clip = songs [numSong];
			audio.Play ();
		}
	}
}
