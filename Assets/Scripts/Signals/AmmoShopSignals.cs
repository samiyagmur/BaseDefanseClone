using Extentions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class AmmoShopSignals : MonoSingleton<AmmoShopSignals>
    {
        public UnityAction<List<Vector3>,GameObject> onGetAmmoContaynerGridPosList = delegate { };
        public UnityAction <float, GameObject> onGetMaxGridAmunt = delegate { };
    }
}