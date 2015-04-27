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
		public GameObject jumper;
		public GameObject pickUp;
		System.Random rnd = new System.Random();
	public int n;
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
				var wall = Instantiate (this.wall);
				wall.transform.parent = newTile.transform;
				wall.transform.localPosition = Vector3.up + UnityEngine.Random.Range (5, 5) * Vector3.left;
				
				var wall1 = Instantiate (this.wall);
				wall1.transform.parent = newTile.transform;
				wall1.transform.localPosition = Vector3.up + UnityEngine.Random.Range (5, 5) * Vector3.right;

				var pickup1 = Instantiate (pickUp);
				pickup1.transform.parent = newTile.transform;
				pickup1.transform.localPosition = UnityEngine.Random.Range (-3, 3) * Vector3.right 
					+ UnityEngine.Random.Range (1, 1) * Vector3.up
						+ UnityEngine.Random.Range (3, 3) * Vector3.forward ;

			}
			// Spawnowanie przeszkod
			
			foreach (var newTile in newTiles) {

					 n = rnd.Next (0, 5);	
				if (n == 0) {
					
					var hazard = Instantiate (terrain);
					hazard.transform.parent = newTile.transform;
					hazard.transform.localPosition = UnityEngine.Random.Range (-4, -4) * Vector3.right;

					var hazard1 = Instantiate (terrain);
					hazard1.transform.parent = newTile.transform;
					hazard1.transform.localPosition = UnityEngine.Random.Range (-3, -3) * Vector3.right;

					var hazard2 = Instantiate (terrain);
					hazard2.transform.parent = newTile.transform;
					hazard2.transform.localPosition = UnityEngine.Random.Range (-4, -4) * Vector3.right 
						+ UnityEngine.Random.Range (1, 1) * Vector3.up;

						
				} else if (n == 1) {
					
					var hazard1 = Instantiate (terrain);
					hazard1.transform.parent = newTile.transform;
					hazard1.transform.localPosition = UnityEngine.Random.Range (4, 4) * Vector3.right;

					var hazard = Instantiate (terrain);
					hazard.transform.parent = newTile.transform;
					hazard.transform.localPosition = UnityEngine.Random.Range (3, 3) * Vector3.right;

					var hazard2 = Instantiate (terrain);
					hazard2.transform.parent = newTile.transform;
					hazard2.transform.localPosition = UnityEngine.Random.Range (4, 4) * Vector3.right 
						+ UnityEngine.Random.Range (1, 1) * Vector3.up;

				}
				else if (n == 2) {
					
					var hazard1 = Instantiate (terrain);
					hazard1.transform.parent = newTile.transform;
					hazard1.transform.localPosition = UnityEngine.Random.Range (0, 0) * Vector3.right
						+ UnityEngine.Random.Range (2, 2) * Vector3.forward ;


					var hazard2 = Instantiate (terrain);
					hazard2.transform.parent = newTile.transform;
					hazard2.transform.localPosition = UnityEngine.Random.Range (0, 0) * Vector3.right
						+ UnityEngine.Random.Range (1, 1) * Vector3.up
							+ UnityEngine.Random.Range (2, 2) * Vector3.forward ;

				}

				else if (n == 3) {
					
					var hazard1 = Instantiate (terrain);
					hazard1.transform.parent = newTile.transform;
					hazard1.transform.localPosition = UnityEngine.Random.Range (4, 4) * Vector3.right;
					
					var hazard = Instantiate (terrain);
					hazard.transform.parent = newTile.transform;
					hazard.transform.localPosition = UnityEngine.Random.Range (3, 3) * Vector3.right;
					
					var hazard2 = Instantiate (terrain);
					hazard2.transform.parent = newTile.transform;
					hazard2.transform.localPosition = UnityEngine.Random.Range (3, 3) * Vector3.right 
						+ UnityEngine.Random.Range (1, 1) * Vector3.up;
					
				}

				else if (n == 4) {
					
					var hazard1 = Instantiate (terrain);
					hazard1.transform.parent = newTile.transform;
					hazard1.transform.localPosition = UnityEngine.Random.Range (-4, -4) * Vector3.right;
					
					var hazard = Instantiate (terrain);
					hazard.transform.parent = newTile.transform;
					hazard.transform.localPosition = UnityEngine.Random.Range (-3, -3) * Vector3.right;
					
					var hazard2 = Instantiate (terrain);
					hazard2.transform.parent = newTile.transform;
					hazard2.transform.localPosition = UnityEngine.Random.Range (-3, -3) * Vector3.right 
						+ UnityEngine.Random.Range (1, 1) * Vector3.up;
					
				}
				/*
			else if (n == 5) {
				var jumper1 = Instantiate (jumper);
				jumper1.transform.parent = newTile.transform;
				jumper1.transform.localPosition = UnityEngine.Random.Range (0, 0) * Vector3.right;
				}
				 */
				
			}
			
		}
	}
}

