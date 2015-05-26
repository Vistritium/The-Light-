using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	protected GameObject go;
	protected AudioSource audio;

	// Use this for initialization
	void Start () {
		go = GameObject.Find ("Settings");
		Settings set = go.GetComponent<Settings> ();
		audio = GetComponent<AudioSource>();
		this.audio.volume = set.Volume;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
