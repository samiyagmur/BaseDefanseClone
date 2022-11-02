using System;
using UnityEngine;

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