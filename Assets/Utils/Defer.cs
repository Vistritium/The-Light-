
using System;
using UnityEngine;
using System.Collections.Generic;


namespace AssemblyCSharp
{

	public class Defer : MonoBehaviour
	{

		public static void DeferAction(Action action, float seconds){
			GameObject.Find ("Systems").GetComponent<Defer> ().DeferAction_ (action, seconds);
		}

		Dictionary<Action, float> actions = new Dictionary<Action, float>();


		private void DeferAction_(Action action, float seconds){
			

			float triggerTime = Time.time + seconds;

			Debug.Log ("Adding");
			actions.Add (action, triggerTime);
		
		}

		List<Action> actionsToRemove = new List<Action>();

		void Update () {
		

			foreach (var entry in actions) {
					if(entry.Value < Time.time){
						actionsToRemove.Add(entry.Key);
				}
			}


			foreach (var actionToRemove in actionsToRemove) {
				actionToRemove.Invoke();
				actions.Remove(actionToRemove);
			}
			actionsToRemove.Clear ();


		}

	}

	


}

