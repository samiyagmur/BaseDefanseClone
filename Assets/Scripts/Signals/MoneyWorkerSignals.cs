using Concreate;
using Data.ValueObject;
using Enums;
using Extentions;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class MoneyWorkerSignals : MonoSingleton<MoneyWorkerSignals>
    {
        public Func<WorkerType, WorkerAITypeData> onGetMoneyAIData = delegate { return null; };
        public UnityAction onSendMoneyPositionToWorkers = delegate { };
        public UnityAction<StackableMoney> onSetStackable = delegate { };
        public UnityAction onThisMoneyTaken = delegate { };
        public UnityAction <Transform> onGenerateMoneyWorker = delegate { };

        public Func<Transform, Transform> onGetTransformMoney = delegate { return null; };
        public Func<Vector3> onSendWaitPosition = delegate { return Vector3.zero; };
        public UnityAction<StackableMoney> onRemoveStackable = delegate { };
    
    }
}