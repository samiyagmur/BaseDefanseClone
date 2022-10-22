﻿using System.Collections;
using UnityEngine;
using Extentions;
using UnityEngine.Events;
using Enums;

namespace Signals
{   
    public class PlayerSignal : MonoSingleton<PlayerSignal>
    {
        public UnityAction<int> onTakePlayerDamage = delegate { };

        public UnityAction<int> onIncreaseHealt = delegate { };

    }
}