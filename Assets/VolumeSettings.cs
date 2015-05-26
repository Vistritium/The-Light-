using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class VolumeSettings : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Settings s = GameObject.Find ("Settings").GetComponent<Settings> ();
		Slider slider = GetComponent<Slider> ();
		slider.value = s.Volume;
		slider.onValueChanged.RemoveAllListeners ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void setup (Slider s)
	{
		s.onValueChanged.RemoveAllListeners ();
		Settings set = GameObject.Find ("Settings").GetComponent<Settings> ();
		s.onValueChanged.AddListener (set.setVolume);
	}
}