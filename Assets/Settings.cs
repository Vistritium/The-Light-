using UnityEngine;using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Settings : Singleton<Settings> {
	
	public float Volume=0.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void setVolume(float value)
	{
		Slider s = GameObject.Find ("MainMenu").transform.GetChild(2).GetComponent<Slider> ();
		Volume = s.value;
		//s.po
		//AudioManager audio = GameObject.Find ("AudioManager").GetComponent<AudioManager>();
		//	audio.setVolume (s.value);
	}
	
}