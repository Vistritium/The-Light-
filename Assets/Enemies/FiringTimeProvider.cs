using System;
using UnityEngine;


namespace AssemblyCSharp
{
	public class FiringTimeProvider : MonoBehaviour
	{

		bool fire = false;

		void Awake(){
			InvokeRepeating ("SetFire", 1f, 3f);
		}


		void SetFire(){
			fire = true;
		}





		public virtual bool ShouldFire(){
			if (fire) {
				fire = false;
				return true;
			}
			return false;
		}
	}
}

