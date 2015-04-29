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
		public int p;
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
				wall.transform.localPosition = Vector3.up + UnityEngine.Random.Range (6, 6) * Vector3.left;
				
				var wall1 = Instantiate (this.wall);
				wall1.transform.parent = newTile.transform;
				wall1.transform.localPosition = Vector3.up + UnityEngine.Random.Range (6, 6) * Vector3.right;
			
			}
			// Spawnowanie przeszkod
			
			foreach (var newTile in newTiles) {

					 n = rnd.Next (0,7 );	
				if (n == 0) {
					// *     *
					// *     *
					//**_____**
					  if (p!=2) {
					//lewa strona
					var hazard = Instantiate (terrain);
					hazard.transform.parent = newTile.transform;
					hazard.transform.localPosition = -4 * Vector3.right
							+ 0.5f*Vector3.up;

					var hazard1 = Instantiate (terrain);
					hazard1.transform.parent = newTile.transform;
						hazard1.transform.localPosition = -3 * Vector3.right + 0.5f*Vector3.up;

					var hazard2 = Instantiate (terrain);
					hazard2.transform.parent = newTile.transform;
					hazard2.transform.localPosition = -4* Vector3.right 
							+  1 * Vector3.forward + 0.5f*Vector3.up;

					var hazard3 = Instantiate (terrain);
					hazard3.transform.parent = newTile.transform;
						hazard3.transform.localPosition = -3 * Vector3.right 
							+ 1 * Vector3.forward + 0.5f*Vector3.up;

					var hazard4 = Instantiate (terrain);
					hazard4.transform.parent = newTile.transform;
					hazard4.transform.localPosition = -3 * Vector3.right 
							+ 2 * Vector3.forward + 0.5f*Vector3.up;
					// prawa strona
					var hazard5 = Instantiate (terrain);
					hazard5.transform.parent = newTile.transform;
						hazard5.transform.localPosition = 4 * Vector3.right + 0.5f*Vector3.up;
					
					var hazard6 = Instantiate (terrain);
					hazard6.transform.parent = newTile.transform;
						hazard6.transform.localPosition = 3 * Vector3.right + 0.5f*Vector3.up;
					
					var hazard7 = Instantiate (terrain);
					hazard7.transform.parent = newTile.transform;
					hazard7.transform.localPosition = 3 * Vector3.right 
							+ 1* Vector3.forward + 0.5f*Vector3.up;
					p = 0;
					}
					else continue;

				} else if (n == 1) {

					//        *  
					//      ***
					//______***  
					//prawa strona
					var hazard1 = Instantiate (terrain);
					hazard1.transform.parent = newTile.transform;
					hazard1.transform.localPosition = 4 * Vector3.right + 0.5f*Vector3.up;

					var hazard = Instantiate (terrain);
					hazard.transform.parent = newTile.transform;
					hazard.transform.localPosition =  3 * Vector3.right + 0.5f*Vector3.up;

					var hazard2 = Instantiate (terrain);
					hazard2.transform.parent = newTile.transform;
					hazard2.transform.localPosition = 4 * Vector3.right 
						+ 1 * Vector3.forward + 0.5f*Vector3.up;

					var hazard3 = Instantiate (terrain);
					hazard3.transform.parent = newTile.transform;
					hazard3.transform.localPosition = 3 * Vector3.right 
						+ 1 * Vector3.forward + 0.5f*Vector3.up;

					var hazard4 = Instantiate (terrain);
					hazard4.transform.parent = newTile.transform;
					hazard4.transform.localPosition = UnityEngine.Random.Range (3, 3) * Vector3.right 
						+ 2 * Vector3.forward + 0.5f*Vector3.up;
					//lewa strona
					/*
					var hazard6 = Instantiate (terrain);
					hazard6.transform.parent = newTile.transform;
					hazard6.transform.localPosition = UnityEngine.Random.Range (1, 1) * Vector3.left; 

					var hazard5 = Instantiate (terrain);
					hazard5.transform.parent = newTile.transform;
					hazard5.transform.localPosition = UnityEngine.Random.Range (1, 1) * Vector3.left 
						+ UnityEngine.Random.Range (1, 1) * Vector3.forward;
						*/
					p = 1;
				}
				else if (n == 2) {
					//
					// ___*
					// __**
					// __*

					var hazard1 = Instantiate (terrain);
					hazard1.transform.parent = newTile.transform;
					hazard1.transform.localPosition =  0 * Vector3.left + 0.5f*Vector3.up;
						


					var hazard2 = Instantiate (terrain);
					hazard2.transform.parent = newTile.transform;
					hazard2.transform.localPosition = 1 * Vector3.left + 0.5f*Vector3.up;

					var hazard3 = Instantiate (terrain);
					hazard3.transform.parent = newTile.transform;
					hazard3.transform.localPosition = 0 * Vector3.left
						+ 1* Vector3.forward  + 0.5f*Vector3.up;

					var hazard4 = Instantiate (terrain);
					hazard4.transform.parent = newTile.transform;
					hazard4.transform.localPosition = 1 * Vector3.left
						+ 1* Vector3.back  + 0.5f*Vector3.up;
					
					p = 2;
				}

				else if (n == 3) {
					//
					//    *
					//*___*______
					var hazard1 = Instantiate (terrain);
					hazard1.transform.parent = newTile.transform;
					hazard1.transform.localPosition =  4 * Vector3.left + 0.5f*Vector3.up;
					
					var hazard = Instantiate (terrain);
					hazard.transform.parent = newTile.transform;
					hazard.transform.localPosition =   1 * Vector3.right + 0.5f*Vector3.up;
					
					var hazard2 = Instantiate (terrain);
					hazard2.transform.parent = newTile.transform;
					hazard2.transform.localPosition = 1 * Vector3.right 
						+ 1 * Vector3.forward + 0.5f*Vector3.up;
					p = 3;
				}

				else if (n == 4) {
				
					continue;

				}

				else if (n == 5) {
					//
					//
					//____****____
					var hazard1 = Instantiate (terrain);
					hazard1.transform.parent = newTile.transform;
					hazard1.transform.localPosition = 0 * Vector3.left + 0.5f*Vector3.up;
					
					var hazard = Instantiate (terrain);
					hazard.transform.parent = newTile.transform;
					hazard.transform.localPosition =  0 * Vector3.right + 0.5f*Vector3.up;
					
					var hazard2 = Instantiate (terrain);
					hazard2.transform.parent = newTile.transform;
					hazard2.transform.localPosition =  1 * Vector3.right + 0.5f*Vector3.up;

					var hazard3 = Instantiate (terrain);
					hazard3.transform.parent = newTile.transform;
					hazard3.transform.localPosition =  1 * Vector3.right + 0.5f*Vector3.up;

					p = 5;
				
				
				}

			else if (n == 6) {
					//
					//*
					//**
					//**

					var hazard1 = Instantiate (terrain);
					hazard1.transform.parent = newTile.transform;
					hazard1.transform.localPosition =  4 * Vector3.left + 0.5f*Vector3.up;
					
					var hazard = Instantiate (terrain);
					hazard.transform.parent = newTile.transform;
					hazard.transform.localPosition =  3 * Vector3.left + 0.5f*Vector3.up;
					
					var hazard2 = Instantiate (terrain);
					hazard2.transform.parent = newTile.transform;
					hazard2.transform.localPosition = 4 * Vector3.left 
						+ 1 * Vector3.forward + 0.5f*Vector3.up;
					
					var hazard3 = Instantiate (terrain);
					hazard3.transform.parent = newTile.transform;
					hazard3.transform.localPosition = 3 * Vector3.left 
						+ 1 * Vector3.forward + 0.5f*Vector3.up;
					
					var hazard4 = Instantiate (terrain);
					hazard4.transform.parent = newTile.transform;
					hazard4.transform.localPosition = 3 * Vector3.left 
						+ 2 * Vector3.forward + 0.5f*Vector3.up;
				
				}
				 
				
			}
			
		}
	}
}

