using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class LightingEffect : MonoBehaviour {


	private GameObject singleLightingCone;
	public int lightingsNumber = 3;
	public float timeOfBolts = 0.5f;
	public float power = 0.05f;
	public bool initOnStart = true;
	public int iterations = 20;
	
	public Vector3 from;
	public Vector3 to;

	private bool initialized = false;

	private List<SingleLightingCone> singleLightinsEffects = new List<SingleLightingCone>();

	// Use this for initialization
	void Start () {
		if (initOnStart) {
			Initialize();
		}

	
	}

	public void Initialize(){
		if (initialized) {
			return;
		}
		initialized = true;
		singleLightingCone = Templates.GetTemplate ("SingleLightingCone");
		var timeOfBoltsPart = timeOfBolts / lightingsNumber;
		for (int i = 0; i < lightingsNumber; i++) {
			var gameObj = Instantiate(singleLightingCone);
			gameObj.SetActive(true);
			gameObj.transform.parent = this.transform;
			gameObj.transform.localPosition = Vector3.zero;
			var singleLightingConeIntantiated = gameObj.GetComponent<SingleLightingCone>();
			singleLightingConeIntantiated.timeOffset = timeOfBoltsPart * i;
			singleLightingConeIntantiated.timeOfBolt = timeOfBolts;
			singleLightingConeIntantiated.flavor = i;
			singleLightingConeIntantiated.from = from;
			singleLightingConeIntantiated.to = to;
			singleLightinsEffects.Add(singleLightingConeIntantiated);
			singleLightingConeIntantiated.power = power;
			singleLightingConeIntantiated.iterations = iterations;
			
			singleLightingConeIntantiated.GenerateNew();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
