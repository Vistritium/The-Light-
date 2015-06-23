using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class TargetProviderProviderBall : TargetProviderProvider {

	public Vector3 bulletDisplacement;

    class TargetPrivderBall : TargetProvider
    {

		public Vector3 bulletDisplacement; 

        private GameObject player;

        private Vector3 target;
        private bool targeted = false;

        void Start()
        {
            this.player = GameObject.Find("Audi");
        }


        public override Vector3 GetTarget()
        {
            if (targeted)
            {

            }
            else
            {
                targeted = true;
				target = player.transform.position + bulletDisplacement;
            }

            return target;
        }
    }

    public override void ProvideTargetProvider(GameObject gameObject)
    {
        gameObject.AddComponent<TargetPrivderBall>();
		gameObject.GetComponent<TargetPrivderBall> ().bulletDisplacement = this.bulletDisplacement;
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
