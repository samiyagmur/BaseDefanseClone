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
        public Func<WorkerSlotType, WorkerAITypeData> onGetMoneyAIData = delegate { return null; };
        public UnityAction onSendMoneyPositionToWorkers = delegate { };
        public UnityAction<Transform> onSetMoneyPosition = delegate { };
        public UnityAction<Transform> onThisMoneyTaken = delegate { };

        public Func<Transform, Transform> onGetTransformMoney = delegate { return null; };
        public Func<Transform, Transform, Transform> OnMyMoneyTaken = delegate { return null; };
    }
}