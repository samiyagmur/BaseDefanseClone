using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Datas.ValueObject
{
    [Serializable]
    public class AmmoWorkerAIData 
    {
        public float Offset;
        public float StackHeigh;
        public float Stackwidth;
        public float MovementSpeed;
        public Transform SpawnPoint;
        public Transform AmmoStore;
        public List<Transform> AmmoContainer;

        

    }
}