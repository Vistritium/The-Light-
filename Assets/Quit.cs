using UnityEngine;
using System.Collections;

public class Quit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public virtual void OnClick () {
		Application.Quit ();
	}
}
