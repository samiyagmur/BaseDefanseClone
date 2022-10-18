using Controllers;
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

        public UnityAction<AmmoStackStatus> onSetAmmoStackStatus = delegate { };

        public Func<TurretKey,GameObject> onGetAmmoToFire = delegate { return null; };

    }
}