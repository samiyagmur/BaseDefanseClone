using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


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
