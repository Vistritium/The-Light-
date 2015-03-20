
using System;
using UnityEngine;


namespace AssemblyCSharp
{
	public class TargetProviderProvider : MonoBehaviour
	{
		public virtual void ProvideTargetProvider(GameObject gameObject){
			var targetProvider = GetComponent<TargetProvider> ();
			if (targetProvider != null) {
				gameObject.AddComponent(typeof(TargetProvider));
			} else {
				gameObject.AddComponent<TargetProvider> ();
			}
		}
	}
}

