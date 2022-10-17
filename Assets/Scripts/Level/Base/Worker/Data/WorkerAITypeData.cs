using System;
using Enums;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Data.ValueObject
{
    [Serializable]
    public class WorkerAITypeData
    {
        public WorkerSlotType WorkerType;
        [HideIf("WorkerType", WorkerSlotType.SoldierAI)]
        public int CapacityOrDamage;
        [HideIf("WorkerType", WorkerSlotType.SoldierAI)]
        public float Speed;
        [HideIf("WorkerType", WorkerSlotType.SoldierAI)]
        public Transform StartTarget;

        [ShowIf("WorkerType", WorkerSlotType.SoldierAI)]
        public SoldierAIData SoldierAIData;
    }

}