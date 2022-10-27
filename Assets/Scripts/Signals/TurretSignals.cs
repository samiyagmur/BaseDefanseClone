using Enums;
using Extentions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class TurretSignals : MonoSingleton<TurretSignals>
    {

      public UnityAction onPressTurretButton =delegate { };

      public UnityAction<TurretId> onReloadStack = delegate { };

      public UnityAction<TurretId,GameObject> onDieEnemy = delegate { };

      public UnityAction<BotCreatType> onGenarateBot = delegate { };

       
        

    }
}