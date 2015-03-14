using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Utils
{
    [Serializable()]
    public class SerializableVector3
    {

        private float x;
        private float y;
        private float z;

        public SerializableVector3() { }
        public SerializableVector3(Vector3 vec3)
        {
            this.x = vec3.x;
            this.y = vec3.y;
            this.z = vec3.z;
        }

        public static implicit operator SerializableVector3(Vector3 vec3)
        {
            return new SerializableVector3(vec3);
        }
        public static explicit operator Vector3(SerializableVector3 serializableVec3)
        {
            return new Vector3(serializableVec3.x, serializableVec3.y, serializableVec3.z);
        }


        public static List<Vector3> ConvertToList(List<SerializableVector3> from)
        {
            var vector3s = new List<Vector3>(from.Count);
            foreach (var serializableVector3 in from)
            {
                vector3s.Add((Vector3)serializableVector3);
            }

            return vector3s;
        }
    }
}
