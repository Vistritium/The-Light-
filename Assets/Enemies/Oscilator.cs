
using System;
using UnityEngine;


namespace AssemblyCSharp
{
	public class Oscilator
	{
		float from;
		float to;
		float lastTime;
		float speed;

		public Oscilator (float from, float to, float speed)
		{
			this.speed = speed;
			this.to = to;
			this.from = from;
			this.lastTime = Time.time;
		}


		public float GetCurrentValue(){
			var myTime = (Time.time - lastTime) * speed;
			var sinus = Mathf.Sin (myTime);

			var result = Mathf.Lerp(from, to, (sinus+1)*0.5f);		
			return result;
		}





	}
}

