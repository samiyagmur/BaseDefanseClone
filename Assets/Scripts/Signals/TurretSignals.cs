using Extentions;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class TurretSignals : MonoSingleton<TurretSignals>
    {

      public UnityAction onPressTurretButton =delegate { }; 
    }
}