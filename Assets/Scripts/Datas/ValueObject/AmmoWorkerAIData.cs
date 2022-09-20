using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class AmmoWorkerAIData 
    {
        public float Offset;
        public float StackHeigh;
        public float Stackwidth;
        public float MovementSpeed;
        public Transform CreatPoint;
        public Transform AmmoStore;
        public List<Transform> AmmoContainer;
    }
}