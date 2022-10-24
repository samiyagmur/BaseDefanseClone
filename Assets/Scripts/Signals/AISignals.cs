using Concreate;
using Extentions;
using System;
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
       
        public Func<Transform> getSpawnTransform;

        public Func<Transform> getRandomTransform;

        public UnityAction<GameObject> onReleaseObjectUpdate = delegate (GameObject arg0) { };

        public UnityAction onGenerateSoldier = delegate{ };

        public UnityAction onEnemyDead = delegate { };

    }
}