using UnityEngine;
using System.Collections;
using System.Linq;
using AssemblyCSharp;
using Assets;
using Assets.Level;

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


                var terrainGenerator = GameObject.Find("TerrainManager").GetComponent<TerrainGenerator>();
                var floats = terrainGenerator.GetPaths();

                var random = Random.Range(0, floats.Count());
                
                var f = (floats[random] - 4) * TileGenerator.tileScale;
                Debug.Log("Random: " + random + " with the path " + f);
                var pos = new Vector3(f, player.transform.position.y, player.transform.position.z);

                target = pos + bulletDisplacement;
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
