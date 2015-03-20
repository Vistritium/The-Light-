
using System;
using UnityEngine;


namespace AssemblyCSharp
{
	public class SpeedProvider : MonoBehaviour
	{

		public float speed = 1f;

		public virtual float GetSpeed(){
			return speed;
		}

	}
}

