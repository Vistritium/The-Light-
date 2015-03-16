using UnityEngine;
using System.Collections;

public class Bolt : MonoBehaviour {

	SingleLightingCone effect;

	// Use this for initialization
	void Start () {
		this.effect = GetComponentInParent<SingleLightingCone> ();
	}
	
	// Update is called once per frame
	void Update () {

		var currentLerp = effect.GetCurrentLerp ();

		currentLerp = 1.0f - currentLerp;



		foreach(Transform child in this.transform){
			setAlpha(child.gameObject, currentLerp);
		}


	}



	private void setAlpha(GameObject gameObject, float alpha){
		var renderer = gameObject.GetComponent<Renderer>();
		
		var currentColor = renderer.material.color;
		currentColor.a = alpha;

		renderer.material.SetFloat("Emission", alpha * 10f);

		renderer.material.color = currentColor;

	}
}
