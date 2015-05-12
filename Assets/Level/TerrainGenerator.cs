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
		public GameObject dashPad;
		public GameObject audiRing;
		public GameObject rail;
		System.Random rnd = new System.Random();
		public int n;
		public int p;
		
		private CameraTargetScript cameraScript;
		private UnitsManager unitManager;
		
		/*
		//ustawienia przeszkod
		public int x01;
		public float y01;
		public int z01;
		public int x02;
		public float y02;
		public int z02;
		public int x11;
		public int y11;
		public int z11;
		public int x21;
		public int y21;
		public int z21;
		public int x31;
		public int y31;
		public int z31;
		*/

		public float[,,] smallObstacles;
		public int[] smallObstacleNumbers;
		public int smallObstaclesAmount;
		
		public int obstacleDistMin = 6;
		public int obstacleDistMax = 15;
		public float obstacleDistSpeedMultiplier = 2;
		public float obstacleDistSpeedBase = 2;
		public float obstacleDistBase = 10;
		public int obstacleFreeTime = 10;
		
		public int ringDistanceMin = 100;
		public int ringDistanceMax = 150;
		public int ringCounter = 300;
		
		public int dashPadDistanceMin = 50;
		public int dashPadDistanceMax = 200;
		public int dashPadCounter = 500;

		public int opponentDistanceMin = 1000;
		public int opponentDistanceMax = 1500;
		public int timeWithOpponent = 500;
		public int opponentCounter = 0;

		public int railLength = 10;
		public int railExtraDist = 50;
		public int railCounter = 0;
		public Vector3 railOffset;

		public int changedStateCounter = 0;
		
		public int obstacleCounter = 50;
		
		private int pathDirection = 0;
		private float pathTimer = 5;
		
		private float[] paths;
		public int[] pathsCounters;

		public enum GeneratorStates
		{
			normal,
			enemy
		}

		public GeneratorStates state = GeneratorStates.normal;
		
		// Use this for initialization
		private void Start()
		{
			var tileGenerator = GetComponent<TileGenerator>();
			tileGenerator.newTilesAdded += NewTilesAdded;
			
			paths = new float[] {1, 4, 7};
			pathsCounters = new int[] {1, 1, 1};
			
			smallObstacles = new float[,,] 
			{ 
				{ {-1, 0, -1}, {0, 0, -1},  {-1, 0, 0}, {0, 0, 0},  {1, 0, 0}, {0, 0, 1},  {1, 0, 1} },
				{ {-1, 0, 1}, {-1, 0, 0},  {0, 0, 0}, {1, 0, 0},  {1, 0, -1}, {0, 0, 0},  {0, 0, 0} },
				{ {-1, 0, 0}, {0, 0, 0},  {1, 0, 0}, {1, 0, 1},  {1, 0, -1}, {0, 0, 0},  {0, 0, 0} }
			};
			
			smallObstacleNumbers = new int[]
			{7, 5, 5};
			
			smallObstaclesAmount = 3;
			
			cameraScript = GameObject.Find ("Player").GetComponent<CameraTargetScript>();
			unitManager = GameObject.Find ("Systems").GetComponent<UnitsManager>();

			opponentCounter = UnityEngine.Random.Range (opponentDistanceMin, opponentDistanceMax);
		}
		
		// Update is called once per frame
		private void Update()
		{
		}
		
		
		
		//called when new tiles are generated
		private void NewTilesAdded(List<GameObject> newTiles)
		{ 	
			// Spawn side walls:
			foreach (var newTile in newTiles) {
				var wall = Instantiate (this.wall);
				wall.transform.parent = newTile.transform;
				wall.transform.localPosition = Vector3.up + 6 * Vector3.left;
				
				var wall1 = Instantiate (this.wall);
				wall1.transform.parent = newTile.transform;
				wall1.transform.localPosition = Vector3.up + 6 * Vector3.right;
				
			}
			
			
			foreach (var newTile in newTiles)
			{
				// Blocks next to walls
				
				// Path
				
				// Enemies
				
				// Pickups
				
				// DashPads
				
				
				for (int i = 0; i < 10; i++)
					CalculationStep(newTile, i);
				
			}
			
		}
		
		private void CalculationStep (GameObject tile, int step)
		{
			int i, j;
			
			// Calculate actual obstacleDistance:
			obstacleFreeTime = (int)((cameraScript.speed + cameraScript.speedAdditional + obstacleDistSpeedBase) * obstacleDistSpeedMultiplier + obstacleDistBase);
			
			// Decrement counters:
			obstacleCounter--;
			
			if (ringCounter > 0)
				ringCounter--;
			
			if (dashPadCounter > 0)
				dashPadCounter--;
			
			for (i = 0; i < 3; i++)
			{
				if (pathsCounters[i] > 0)
					pathsCounters[i]++;
			}

			if (opponentCounter > 0)
			{
				opponentCounter--;

				if (opponentCounter == 0)
				{	
					changedStateCounter = timeWithOpponent;
					state = GeneratorStates.enemy;

					unitManager.SpawnLaserMachine(UnitsManager.LaserMachineType.SHORT_DURATION);
				}
			}

			if (changedStateCounter > 0)
			{
				changedStateCounter--;

				if (changedStateCounter == 0)
				{
					opponentCounter = UnityEngine.Random.Range (opponentDistanceMin, opponentDistanceMax);
					state = GeneratorStates.normal;

					//unitManager.newLaserMachine.SendMessage("Remove");
				}
			}

			if (opponentCounter < railExtraDist)
			{
				railCounter--;

				if (railCounter <= 0)
				{
					CreateRailSegment (tile, railOffset, step);
					railCounter = railLength;
				}
			}
			else
			{
				railCounter = 0;
			}


			#region Set obstacles[]' values, where new obstacles should be
			
			// If it is time to place an obstacle, do it:
			if (obstacleCounter == 0)
			{
				bool[] obstacles = new bool[] {false, false, false};

				switch (state)
				{
				case GeneratorStates.normal:
					obstacles = PlacementAlgorithm1(obstacles);
					break;
				case GeneratorStates.enemy:
					obstacles = PlacementAlgorithm2(obstacles);
					break;
				}

				
				#endregion
				
				
				// For each one of chosen lanes, create a wall there:
				for (i = 0; i < 3; i++)
				{
					if (obstacles[i] == true)
					{
						CreateSmallObstacle(tile, new Vector3(paths[i] - 4f, 0.5f, step - 4.5f));
						
						//pathsCounters[i] = 8;
					}
				}
				
				// Place pickups:
				if (ringCounter == 0)
				{
					if (PlaceObject(obstacles, tile, audiRing, step, 1.5f) == 1)
						ringCounter = UnityEngine.Random.Range(ringDistanceMin, ringDistanceMax);
				}
				else if (dashPadCounter == 0)
				{
					if (PlaceObject(obstacles, tile, dashPad, step, 0f) == 1)
						dashPadCounter = UnityEngine.Random.Range(dashPadDistanceMin, dashPadDistanceMax);
				}
				
				// Now, mark the possible driving routes for future iterations:
				for (i = 0; i < 3; i++)
				{
					if (obstacles[i] == false)
					{
						//pathsCounters[i] = 1;
					}
					else
					{
						pathsCounters[i] = 1;
					}
				}
			}
			
			/*
			// Create obstacles:
			if (step == 3 || step == 7)
			{
				//CreateBlock(tile, new Vector3(path - 4f, 2.5f, step - 4.5f));
			}
			*/
		}

		private void CreateRailSegment(GameObject tile, Vector3 pos, int step)
		{
			var wall = Instantiate (rail);
			wall.transform.parent = tile.transform;
			wall.transform.localPosition = pos + new Vector3(0, 0, step);
		}
		
		
		private void CreateSmallObstacle(GameObject tile, Vector3 pos)
		{
			int chosen = UnityEngine.Random.Range (0, smallObstaclesAmount);
			
			for (int i = 0; i < smallObstacleNumbers[chosen]; i++)
			{
				CreateBlock(tile, new Vector3(pos.x + smallObstacles[chosen, i, 0], pos.y + smallObstacles[chosen, i, 1], pos.z + smallObstacles[chosen, i, 2]), terrain);
			}
		}
		
		private void CreateBlock(GameObject tile, Vector3 pos, GameObject obj)
		{
			if (pos.x % 1 != 0)
			{
				pos.x = Mathf.Round(pos.x);
			}
			
			var wall = Instantiate (obj);
			wall.transform.parent = tile.transform;
			wall.transform.localPosition = pos;
		}

		private bool[] PlacementAlgorithm1(bool[] obstacles)
		{
			int i, j;

			// Firstly, reset the counter:
			obstacleCounter = UnityEngine.Random.Range (obstacleDistMin, obstacleDistMax);
			
			//bool[] obstacles = new bool[] {false, false, false};
			bool done = false;
			
			// Sum up, how many driving routes are there:
			int sum = 0;
			
			for (i = 0; i < 3; i++)
				if (pathsCounters [i] > obstacleFreeTime)
					sum++;
			
			// If exactly two paths are free, occupy one of them:
			if (sum == 2)
				for (i = 0; i < 2; i++) {
					if (pathsCounters [i] > obstacleFreeTime && pathsCounters [i + 1] > obstacleFreeTime) {
						obstacles [i + UnityEngine.Random.Range (0, 2)] = true;
						done = true;
					
						for (j = 0; j < 3; j++) {
							if (pathsCounters [j] <= obstacleFreeTime)
								obstacles [j] = true;
						}
					}
				}
			
			// Else, if two paths on the sides are free, do nothing .:
			if (done == false && pathsCounters [0] > obstacleFreeTime && pathsCounters [2] > obstacleFreeTime && pathsCounters [1] <= obstacleFreeTime) {
				//if (UnityEngine.Random.Range(0, 2) == 1)
				//	obstacles[1] = true;
				
				done = true;
			}
			
			// If you have one pathway, that player had to take, create one obstacle around it:
			if (done == false && sum == 1) {
				j = UnityEngine.Random.Range (0, 3 - sum);
				i = 0;
				while (done == false && i < 3) {
					if (pathsCounters [i] <= obstacleFreeTime) {
						if (j == 0) {
							//sum--;
							obstacles [i] = true;
							done = true;
						} else
							j--;
					}
					
					i++;
				}
				
				done = true;
			}
			
			// If you have free field, create one or two obstacles:
			if (done == false) {
				if (UnityEngine.Random.Range (0, 2) == 1) {
					// Place one obstacle:
					obstacles [UnityEngine.Random.Range (0, 3)] = true;
					
					done = true;
				} else {
					// Place two obstacles:
					
					i = UnityEngine.Random.Range (0, 3);
					
					if (i < 2 && pathsCounters [1] < obstacleFreeTime * 2)
						i = 2;
					
					if (i == 0 && pathsCounters [0] < obstacleFreeTime * 2)
						i++;
					
					if (i == 1 && pathsCounters [2] < obstacleFreeTime * 2)
						i++;
					
					obstacles [i] = true;
					if (i < 2)
						obstacles [i + 1] = true;
					else
						obstacles [0] = true;
					
					done = true;
				}
			}

			return obstacles;
		}

		private bool[] PlacementAlgorithm2(bool[] obstacles)
		{
			int i, j;
			
			// Firstly, reset the counter:
			obstacleCounter = UnityEngine.Random.Range (obstacleDistMin, obstacleDistMax);

			//bool[] obstacles = new bool[] {false, false, false};
			bool done = false;
			
			// Choose the driving route, that has been obstructed most lately:
			int sum = 0;

			for (i = 1; i < 3; i++)
				if (pathsCounters [i] < pathsCounters[sum])
					sum=i;

			if (pathsCounters [sum] > obstacleFreeTime * 2)
			{
				j = UnityEngine.Random.Range (0, 2);

				i = 0;
				while (done == false && i < 3)
				{
					if (i != sum)
					{
						if (j == 0)
						{
							obstacles[i] = true;
							done = true;
						}
						else
						{
							j--;
						}
					}

					i++;
				}
			}
			else
			{
				done = true;
			}
			
			return obstacles;
		}

		// Place object like pickup or dashpad:
		private int PlaceObject(bool[] obstacles, GameObject tile, GameObject obj, int step, float height)
		{
			int sum = 0;
			int j = 3;
			int i;
			for (i = 0; i < 3; i++)
			{
				if (obstacles[i] == false && pathsCounters[i] > 0)
				{
					sum++;
				}
				else if (sum > 1)
				{
					j = i;
					i = 3;
				}
				else
				{
					sum = 0;
				}
			}
			
			if (sum > 1)
			{
				if (UnityEngine.Random.Range(0, 2) == 1)
				{
					CreateBlock(tile, new Vector3(paths[j - 1] - 4f, height, step - 4.5f), obj);
				}
				else
				{
					CreateBlock(tile, new Vector3(paths[j - sum] - 4f, height, step - 4.5f), obj);
				}
				
				return 1;
			}
			
			return 0;
		}
	}
}

