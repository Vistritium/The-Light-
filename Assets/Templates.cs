using System;
using UnityEngine;


namespace AssemblyCSharp
{
	public static class Templates
	{



		public static GameObject GetTemplate(string name){
			if (name == null) {
				throw new ArgumentException(string.Format("Name cannot be null"));
			}
			if(name.Length == 0){
				throw new ArgumentException(string.Format("Name is empty"));
			}
			var template = GameObject.Find ("Templates");

			foreach(Transform child in template.transform){
				if(child.name == name){
					return child.gameObject;
				}
			}

			throw new ArgumentException(string.Format("Name {0} does not exist in tamplates. Check Templates object", name));
		}


	}
}

