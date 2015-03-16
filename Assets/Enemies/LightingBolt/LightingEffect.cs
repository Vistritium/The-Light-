using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightingEffect : MonoBehaviour {


	public GameObject singleLightingCone;
	public int lightingsNumber = 3;
	public float timeOfBolts = 0.5f;

	private List<SingleLightingCone> singleLightinsEffects = new List<SingleLightingCone>();

	// Use this for initialization
	void Start () {

		var timeOfBoltsPart = timeOfBolts / lightingsNumber;

		for (int i = 0; i < lightingsNumber; i++) {
			var gameObj = Instantiate(singleLightingCone);
			gameObj.transform.parent = this.transform;
			gameObj.transform.localPosition = Vector3.zero;
			var singleLightingConeIntantiated = gameObj.GetComponent<SingleLightingCone>();
			singleLightingConeIntantiated.timeOffset = timeOfBoltsPart * i;
			singleLightingConeIntantiated.timeOfBolt = timeOfBolts;
			singleLightingConeIntantiated.flavor = i;
			singleLightinsEffects.Add(singleLightingConeIntantiated);
			

		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
