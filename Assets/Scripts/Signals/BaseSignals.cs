using Enums;
using Extentions;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class BaseSignals : MonoSingleton<BaseSignals>
    {
        public UnityAction<BaseRoomTypes> onChangeExtentionVisibility = delegate { };

    }
}