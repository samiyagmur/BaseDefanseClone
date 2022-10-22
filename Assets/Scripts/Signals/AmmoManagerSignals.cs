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

        public UnityAction <TurretStackController> onSetTurretStackControllers = delegate { };
        
        public UnityAction<AmmoStackStatus> onSetAmmoStackStatus = delegate { };

        public UnityAction<int> onIncreaseAmmoWorkerSpeed = delegate { };

        public UnityAction<int> onIncreaseAmmoWorkerCapasity = delegate { };

        public Func<TurretKey,GameObject> onGetAmmoToFire = delegate { return null; };

        public Func<TurretKey,int> onGetCurrentTurretStackCount = delegate { return 0; };

    }
}