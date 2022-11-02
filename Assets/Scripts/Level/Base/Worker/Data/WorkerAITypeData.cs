using Enums;
using System;

namespace Data.ValueObject
{
    [Serializable]
    public class WorkerAITypeData
    {
        public WorkerType WorkerType;

        public int CapacityOrDamage;

        public float Speed;

        public int MaxWorkerAmount;

        public float MaxSpeed;
    }
}