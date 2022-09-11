﻿using System.Collections;
using UnityEngine;
using Extentions;
using UnityEngine.Events;
using Keys;

namespace Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction<HorizontalInputParams> onInputDragged = delegate { };

    }
}