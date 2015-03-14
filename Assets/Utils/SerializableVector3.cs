using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Utils
{

    [Serializable()]
    public class SerializableVector3
    {
        private float x;
        private float y;
        private float z;

        public SerializableVector3()
        {
        }

        public SerializableVector3(Vector3 vec3)
        {
            this.x = vec3.x;
            this.y = vec3.y;
            this.z = vec3.z;
        }


        public static Vector3 ToVector(SerializableVector3 from)
        {
            return new Vector3(from.x, from.y, from.z);
        }

        public static List<Vector3> ToVectorList(List<SerializableVector3> from)
        {
            var serializableVector3s = new List<Vector3>(@from.Count);
            foreach (var serializableVector3 in from)
            {
                serializableVector3s.Add(ToVector(serializableVector3));
            }

            return serializableVector3s;
        }

        public static List<SerializableVector3> FromVectorList(List<Vector3> from)
        {
            var result = new List<SerializableVector3>(@from.Count);
            foreach (var iter in from)
            {
                result.Add(new SerializableVector3(iter));
            }

            return result;
        }

        public static List<List<SerializableVector3>> FromVectorListList(List<List<Vector3>> from)
        {
            var result = new List<List<SerializableVector3>>(@from.Count);
            foreach (var iter in from)
            {
                result.Add(FromVectorList(iter));
            }

            return result;
        }


        public static List<List<Vector3>> ToVectorListList(List<List<SerializableVector3>> from)
        {
            var result = new List<List<Vector3>>(@from.Count);
            foreach (var serializableVector3 in from)
            {
                result.Add(ToVectorList(serializableVector3));
            }

            return result;
        } 
    }
}