using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class ToPlayerTargerProviderProvider : TargetProviderProvider {

	private class ToPlayerTargetProvider : TargetProvider {

		Vector3 target;

		private float randomHorizontal = 5;
		private float randomVertical = 5;

		private static System.Random random = new System.Random();

		void Awake(){
			var player = GameObject.FindGameObjectsWithTag ("Player")[0];

			var randomHorizontalVec = (float)(randomHorizontal * random.NextDouble () - randomHorizontal * 0.5) * Vector3.left;
			var randomVerticalVec = (float)(randomVertical * random.NextDouble () - randomVertical * 0.5) * Vector3.forward;

			target = player.transform.position + Vector3.forward * 7 + randomHorizontalVec + randomVerticalVec;
		}

		public override Vector3 GetTarget(){
			//Debug.Log ("Returning " + target);
			return target;
		}

	}

	public override void ProvideTargetProvider(GameObject gameObject){
		gameObject.AddComponent<ToPlayerTargetProvider> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
