using System.Collections.Generic;
using System.Linq;
using Assets.Level;
using UnityEngine;
using System;

namespace Assets
{
    public class TerrainGenerator : MonoBehaviour
    {
		public GameObject terrain;
		public GameObject wall;
		public GameObject wall1;

        // Use this for initialization
        private void Start()
        {
            var tileGenerator = GetComponent<TileGenerator>();
            tileGenerator.newTilesAdded += NewTilesAdded;
        }

        // Update is called once per frame
        private void Update()
        {
        }


        //called when new tiles are generated
        private void NewTilesAdded(List<GameObject> newTiles)
        {  
			  
				foreach (var newTile in newTiles) {
					Debug.Log("adding new tile");
					var hazard = Instantiate (terrain);
					hazard.transform.parent = newTile.transform;
					hazard.transform.localPosition = UnityEngine.Random.Range(-3, 3) * Vector3.right;
				    var wall = Instantiate (this.wall);
			    	wall.transform.parent = newTile.transform;
					wall.transform.localPosition = UnityEngine.Random.Range(1, 1)  * Vector3.up + UnityEngine.Random.Range(5, 5) * Vector3.left;
					
				var wall1 = Instantiate (this.wall);
				wall1.transform.parent = newTile.transform;
				wall1.transform.localPosition = UnityEngine.Random.Range(1, 1)  * Vector3.up + UnityEngine.Random.Range(5, 5) * Vector3.right;

			}
			
			Debug.Log("New tiles added: " + newTiles.Select(x => x.ToString()).Aggregate((x1, x2) => x1 + " " + x2));
        }
    }
}