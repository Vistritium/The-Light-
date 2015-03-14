
using System;
using UnityEngine;
using System.Collections.Generic;


namespace AssemblyCSharp
{
	public static class BoltPointsGenerator
	{

		static System.Random random = new System.Random ();

		public static void GenerateAndAddPoints(Vector3 begening, Vector3 end, int iterations, float power, List<List<Vector3>> addTo){
			if (iterations == 0) {
				return;
			}
            Debug.Log(string.Format("Iterations left:{0}", iterations));

			var middlePoint = Vector3.Lerp (begening, end, 0.5f);

			var rotation = Quaternion.AngleAxis ((float)random.NextDouble() * 360f, end - begening);

			var cross = Vector3.Cross (begening, end);

			Vector3 rotated = rotation * cross;
			rotated.Normalize ();

			var distance = Vector3.Distance (begening, end) * 0.1f;

			middlePoint = middlePoint + rotated * distance;

			var toAdd = new List<Vector3> (){begening, middlePoint, end};

			addTo.Add (toAdd);

			var oneHalf = iterations / 2;
			var secondHalf = iterations - oneHalf;

			GenerateAndAddPoints (begening, middlePoint, oneHalf, power, addTo);
			GenerateAndAddPoints (begening, middlePoint, secondHalf, power, addTo);

		}

		public static List<List<Vector3>> GeneratePoints(Vector3 begening, Vector3 end, float power, int iterations){
			var result = new List<List<Vector3>>(iterations);

			result.Add(new List<Vector3>(){begening, end});

			GenerateAndAddPoints (begening, end, iterations, power, result);

			Debug.Log("Points: " + begening + " " + end);

			return result;
		}



	}
}

