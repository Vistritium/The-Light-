using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class ElectricLaser : MonoBehaviour {

	private Vector3 begeningFrom = Vector3.left * 10;
	private Vector3 begeningTo = Vector3.right * 10;
	private float begeningDistance;

	public Vector3 from;
	public Vector3 to;

	private LightingEffect lightingEffect;
	private GameObject lightinEffectGameObject;

	// Use this for initialization
	void Start () {
		from = begeningFrom;
		to = begeningTo;
		begeningDistance = Vector3.Distance (begeningFrom, begeningTo);

		lightinEffectGameObject = Instantiate(Templates.GetTemplate("LightingEffectObject"));
		lightinEffectGameObject.SetActive (true);

		this.lightingEffect = lightinEffectGameObject.GetComponent<LightingEffect>();

		lightingEffect.from = this.from;
		lightingEffect.to = this.to;

		lightinEffectGameObject.transform.parent = this.transform;

		lightingEffect.Initialize ();
		

		lightinEffectGameObject.transform.Rotate(new Vector3 (0f, 90f, 0f));

	}
	
	// Update is called once per frame
	void Update () {
	

			UpdatePositions();



	}


	public void UpdatePositions(){

		this.transform.position = Vector3.Lerp (from, to, 0.5f);
		
		if (lightinEffectGameObject != null) {
			foreach (Transform transform in lightinEffectGameObject.transform) {
				transform.localScale = new Vector3(Vector3.Distance(from, to) / begeningDistance, 1, 1);
			}
		}

		this.transform.LookAt (from);
		



	}


	public static ElectricLaser GetElectricLaser(){
		var newGameObject = new GameObject ();
		newGameObject.AddComponent<ElectricLaser> ();

		return newGameObject.GetComponent<ElectricLaser> ();
	}

}
