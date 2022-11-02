using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class PoolData
    {
        public GameObject ObjectType;
        public int InitalAmount;
        public bool IsDynamic;
    }
}