using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class TargetProviderProviderBall : TargetProviderProvider {

    class TargetPrivderBall : TargetProvider
    {
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
                target = player.transform.position + Vector3.forward*125;
            }

            return target;
        }
    }

    public override void ProvideTargetProvider(GameObject gameObject)
    {
        gameObject.AddComponent<TargetPrivderBall>();
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
