using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extentions;
using UnityEngine.Events;
using System;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onLevelInitialize = delegate  { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onReset = delegate { };
        public UnityAction<int> onUpdateMoneyScore = delegate { };
        public UnityAction<int> onUpdateGemScore = delegate { };
        public UnityAction onAplicationPause= delegate { };
        public Func<int> onGetCurrentMoney = delegate { return 0; };
        public Func<int> onGetCurrentDiamond = delegate { return 0; };
        public Func<bool> onHasEnoughMoney = delegate { return false; };
        public UnityAction onStopMoneyPayment = delegate { };
        public UnityAction onStartMoneyPayment = delegate { };
        public UnityAction onFailed = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction onResetPlayerStack = delegate { };





    }
}
