using System;
using UnityEngine;


namespace AssemblyCSharp
{
	public class FiringTimeProvider : MonoBehaviour
	{

		bool fire = false;
		bool removing = false;

		void Awake(){
			InvokeRepeating ("SetFire", 1f, 3f);
		}


		void SetFire(){
			fire = true;
		}

		private void Remove(){
			removing = true;
			Debug.Log ("Shooting for following machine disabled");
		}



		public virtual bool ShouldFire(){
			if (fire && !removing) {
				fire = false;
				return true;
			}
			return false;
		}
	}
}

