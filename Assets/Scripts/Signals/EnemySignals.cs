using System.Collections;
using UnityEngine;
using Extentions;
using UnityEngine.Events;
using System;
using Data.ValueObject;
using Enums;

namespace Signals
{
    public class EnemySignals : MonoSingleton<EnemySignals>
    {
        public UnityAction onOpenPortal = delegate { };
    }
}