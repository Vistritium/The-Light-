using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class SpeedProviderProviderBall : SpeedProviderProvider {

    class SpeedProviderBall : SpeedProvider
    {

		public float speed;

        public override float GetSpeed()
        {
			return speed;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void ProvideSpeedProvider(GameObject gameObject)
    {
        gameObject.AddComponent<SpeedProviderBall>();
		gameObject.GetComponent<SpeedProviderBall> ().speed = this.speed;
    }
}
