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

		//ustawienia przeszkod
		public int x01;
		public float y01;
		public int z01;
		public int x02;
		public float y02;
		public int z02;
		public int x03;
		public float y03;
		public int z03;
		public int x04;
		public float y04;
		public int z04;
		public int x05;
		public float y05;
		public int z05;

		public int x11;
		public float y11;
		public int z11;
		public int x12;
		public float y12;
		public int z12;
		public int x13;
		public float y13;
		public int z13;

		public int x21;
		public float y21;
		public int z21;
		public int x22;
		public float y22;
		public int z22;
		public int x23;
		public float y23;
		public int z23;


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

					 n = rnd.Next (0,3);	
				if (n == 0) {

					var hazard = Instantiate (terrain);
					hazard.transform.parent = newTile.transform;
					hazard.transform.localPosition = x01 * Vector3.right
							+ y01*Vector3.up + z01*Vector3.forward;

					var hazard1 = Instantiate (terrain);
					hazard1.transform.parent = newTile.transform;
						hazard1.transform.localPosition = x02 * Vector3.right + y02*Vector3.up + z02*Vector3.forward;

						var hazard2 = Instantiate (terrain);
						hazard2.transform.parent = newTile.transform;
						hazard2.transform.localPosition = x03 * Vector3.right + y03*Vector3.up + z03*Vector3.forward;
					/*
						var hazard3 = Instantiate (terrain);
						hazard3.transform.parent = newTile.transform;
						hazard3.transform.localPosition = x04 * Vector3.right + y04*Vector3.up + z04*Vector3.forward;

						
						var hazard5 = Instantiate (terrain);
						hazard5.transform.parent = newTile.transform;
						hazard5.transform.localPosition = x05 * Vector3.right + y05*Vector3.up + z05*Vector3.forward;
*/
					p=0;
					} 
						else if (n == 1) {
					var hazard = Instantiate (terrain);
					hazard.transform.parent = newTile.transform;
					hazard.transform.localPosition = x11 * Vector3.right
						+ y11*Vector3.up + z11*Vector3.forward;
					
					var hazard1 = Instantiate (terrain);
					hazard1.transform.parent = newTile.transform;
					hazard1.transform.localPosition = x12 * Vector3.right + y12*Vector3.up + z12*Vector3.forward;
					
					var hazard2 = Instantiate (terrain);
					hazard2.transform.parent = newTile.transform;
					hazard2.transform.localPosition = x13 * Vector3.right + y13*Vector3.up + z13*Vector3.forward;
					/*
						var hazard3 = Instantiate (terrain);
						hazard3.transform.parent = newTile.transform;
						hazard3.transform.localPosition = x14 * Vector3.right + y14*Vector3.up + z14*Vector3.forward;

						
						var hazard5 = Instantiate (terrain);
						hazard5.transform.parent = newTile.transform;
						hazard5.transform.localPosition = x15 * Vector3.right + y15*Vector3.up + z15*Vector3.forward;
*/
					p=1;
				}

				else if (n == 2) {
					//
					// ___*
					// __**
					// __*

					var hazard = Instantiate (terrain);
					hazard.transform.parent = newTile.transform;
					hazard.transform.localPosition = x21 * Vector3.right
						+ y21*Vector3.up + z21*Vector3.forward;
					
					var hazard1 = Instantiate (terrain);
					hazard1.transform.parent = newTile.transform;
					hazard1.transform.localPosition = x22 * Vector3.right + y22*Vector3.up + z22*Vector3.forward;
					
					var hazard2 = Instantiate (terrain);
					hazard2.transform.parent = newTile.transform;
					hazard2.transform.localPosition = x23 * Vector3.right + y23*Vector3.up + z23*Vector3.forward;

					
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

