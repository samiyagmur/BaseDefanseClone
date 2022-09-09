using System.Collections;
using UnityEngine;

namespace Abstraction
{
    public abstract class StacableValue: MonoBehaviour
    {
        public int Capacity;
        public int Offset;

        protected StacableValue(int capacity, int offset)
        {
            Capacity = capacity;
            Offset = offset;
        }
    }
}