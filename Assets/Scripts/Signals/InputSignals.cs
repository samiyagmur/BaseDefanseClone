using System.Collections;
using UnityEngine;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction onInputDragged = delegate { };

    }
}