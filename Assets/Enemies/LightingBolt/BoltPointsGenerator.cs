//#define GENERATE_NEW
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Utils;
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

		    var middlePointSelector = (float)random.NextDouble()*0.4f;

		    var middlePoint = Vector3.Lerp (begening, end, 0.3f + middlePointSelector);

			var rotation = Quaternion.AngleAxis ((float)random.NextDouble() * 360f, end - begening);

		    var toEnd = end - begening;
		    var someRandom = new Vector3(toEnd.x + 2, toEnd.y - 2, toEnd.z - 9);

		    var cross = Vector3.Cross(toEnd, someRandom);

			Vector3 rotated = rotation * cross;
			rotated.Normalize ();

			var distance = Vector3.Distance (begening, end) * power;

			middlePoint = middlePoint + rotated * distance;

			var toAdd = new List<Vector3> (){begening, middlePoint, end};

			addTo.Add (toAdd);

			var oneHalf = iterations / 2;
			var secondHalf = iterations - oneHalf - 1;

			GenerateAndAddPoints (begening, middlePoint, oneHalf, power, addTo);
            GenerateAndAddPoints(middlePoint, end, secondHalf, power, addTo);

		}



		public static List<List<Vector3>> GeneratePoints(Vector3 begening, Vector3 end, float power, int iterations, int flavor)
		{
		    var name = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", begening.x, begening.y, begening.z, end.x, end.y, end.z,
		        power, iterations, flavor);
		    var path = string.Format("{0}/{1}", Application.persistentDataPath, "boltsData");
		    var filePath = string.Format("{0}/{1}", path, name);

		    bool generateNew = false;

#if GENERATE_NEW
            generateNew = true;
#endif

            if (File.Exists(filePath) && !generateNew)
		    {
                Debug.Log("Deserializing");
                BinaryFormatter bf = new BinaryFormatter();
                var file = File.Open(filePath, FileMode.Open);
		        List<List<Vector3>> result = SerializableVector3.ToVectorListList((List<List<SerializableVector3>>) bf.Deserialize(file));
                file.Close();
                
		        return result;
		    }
		    else
		    {
                Debug.Log("Serializing");
                var result = new List<List<Vector3>>(iterations);

                result.Add(new List<Vector3>() { begening, end });

                GenerateAndAddPoints(begening, end, iterations, power, result);

               

                BinaryFormatter bf = new BinaryFormatter();

		        Directory.CreateDirectory(path);
		        FileStream file = File.Create(filePath);
                bf.Serialize(file, SerializableVector3.FromVectorListList(result));
                file.Close();
                
                return result;
		    }
            



			
		}



	}
}

