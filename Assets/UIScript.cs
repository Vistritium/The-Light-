using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	public Text pauseText;
	public Text scoreText;
	public Image speedometerImage;
	public Image needleImage;

	// Use this for initialization
	void Start () {
		speedometerImage.enabled = false;
		needleImage.enabled = false;
		scoreText.enabled = false;

		Invoke ("ShowSpeedometer", 3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ShowSpeedometer() {
		speedometerImage.enabled = true;
		needleImage.enabled = true;
		scoreText.enabled = true;
		pauseText.enabled = false;
	}
}
