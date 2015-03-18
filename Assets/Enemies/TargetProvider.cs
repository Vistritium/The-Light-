using System;
using UnityEngine;


namespace AssemblyCSharp
{
	public class TargetProvider : MonoBehaviour
	{
		public virtual Vector3 GetTarget(){
			return Vector3.zero;
		}
	}
}

