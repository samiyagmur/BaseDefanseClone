using AIBrain;
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

        public UnityAction< AmmoDropZoneStackController> onSetTurretStackList = delegate { };

        public UnityAction<AmmoStackStatus> onSetAmmoStackStatus = delegate { };

        public UnityAction<AmmoWorkerBrain> onAmmoworkerEnterAmmoWareHouse = delegate { };

    }
}