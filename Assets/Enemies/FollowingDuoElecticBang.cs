using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class FollowingDuoElecticBang : MonoBehaviour {

	public float fireDelay = 3f;
	public float fireLength = 3f;

	private FollowingMachine leftWing;
	private FollowingMachine rightWing;

	private LightingEffect lightingEffect;
	
	

	// Use this for initialization
	void Start () {

		var lightinEffectGameObject = Instantiate(Templates.GetTemplate("LightingEffectObject"));
		lightinEffectGameObject.SetActive (true);

		this.lightingEffect = lightinEffectGameObject.GetComponent<LightingEffect>();

		var player = GameObject.Find ("Player");
		this.transform.parent = player.transform;
		this.transform.localPosition = Vector3.zero;

		 

		var templated = Templates.GetTemplate ("FollowingMachine");

		leftWing = Instantiate (templated).GetComponent<FollowingMachine>();
		leftWing.displacementFromPlayer = Vector3.left * 10 + Vector3.up * 7 + Vector3.forward * 10;
		leftWing.backDisplacement = Vector3.back * 20;
		leftWing.gameObject.SetActive (true);



		rightWing = Instantiate (templated).GetComponent<FollowingMachine>();
		rightWing.backDisplacement = Vector3.back * 20;
		rightWing.displacementFromPlayer = Vector3.right * 10 + Vector3.up * 7 + Vector3.forward * 10;
		rightWing.gameObject.SetActive (true);

		lightinEffectGameObject.transform.parent = leftWing.transform;

		lightingEffect.from = leftWing.displacementFromPlayer;
		lightingEffect.to = rightWing.displacementFromPlayer;




		lightingEffect.Initialize ();

		lightinEffectGameObject.transform.position = -leftWing.displacementFromPlayer;
		Debug.Log ("position: " + Vector3.Lerp (leftWing.transform.position, rightWing.transform.position, 0.5f));

		lightingEffect.gameObject.SetActive (false);

		leftWing.onPosition += delegate {
			Invoke("Fire", fireDelay);
		};

	
	}


	private void Fire(){
		Invoke ("StopFire", fireLength);
		lightingEffect.gameObject.SetActive (true);


	}

	private void StopFire(){
		lightingEffect.gameObject.SetActive (false);
		Invoke("Fire", fireDelay);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
