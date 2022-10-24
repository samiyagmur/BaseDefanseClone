using System.Collections;
using UnityEngine;
using Extentions;
using UnityEngine.Events;
using Enums;
using System;

namespace Signals
{   
    public class PlayerSignal : MonoSingleton<PlayerSignal>
    {
        public UnityAction<int> onTakePlayerDamage = delegate { };

        public UnityAction<int> onIncreaseHealt = delegate { };

        public UnityAction<Transform> onSetWeaponTransform = delegate { };
        
    }
}