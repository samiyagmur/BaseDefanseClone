using Enums;
using Extentions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class AmmoManagerSignals : MonoSingleton<AmmoManagerSignals>
    {
        public UnityAction <Transform> onPlayerEnterAmmoWorkerCreaterArea = delegate { };

        public Func<GameObject> onSetConteynerList = delegate {return null;};

        public UnityAction<AmmoStackStatus> onAmmoStackStatus=delegate { };
    }
}