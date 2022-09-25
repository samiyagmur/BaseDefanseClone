using Extentions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class AmmoManagerSignals : MonoSingleton<AmmoManagerSignals>
    {

        public UnityAction onGetCurrentContaynerInfo = delegate { };

        public UnityAction<int> onContaynerStackFull = delegate {  };

        public UnityAction <GameObject> onSetConteynerList = delegate { };

        public UnityAction<float> onCurrentContaynerAmount =delegate {  };  

    }
}