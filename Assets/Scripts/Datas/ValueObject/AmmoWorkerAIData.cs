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

        public float MovementSpeed;
        public Transform AmmoWareHouse;
        public GameObject AmmoWorker;
        public GameObject Ammo;
        public int MaxStackCount;
    }
}