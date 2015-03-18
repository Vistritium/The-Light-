
using System;
using UnityEngine;


namespace AssemblyCSharp
{
	public class SpeedProvider : MonoBehaviour
	{
		float speed = 5f;

		public virtual float GetSpeed(){
			return speed;
		}

	}
}

