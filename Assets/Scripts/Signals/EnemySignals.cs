using System.Collections;
using UnityEngine;
using Extentions;
using UnityEngine.Events;

namespace Assets.Scripts.Signals
{
    public class EnemySignals : MonoSingleton<EnemySignals>
    {
        public UnityAction OnDetectedPlayer = delegate { };

    }
}