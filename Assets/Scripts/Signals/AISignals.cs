using Concreate;
using Extentions;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class AISignals : MonoSingleton<AISignals>
    {
        public UnityAction onSoldierActivation = delegate { };
        public UnityAction onSoldierAmountUpgrade = delegate { };
        public UnityAction<StackableMoney> onSetMoneyPosition = delegate { };

    }
}