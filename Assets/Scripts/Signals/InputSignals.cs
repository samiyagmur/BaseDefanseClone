using System.Collections;
using UnityEngine;
using Extentions;
using UnityEngine.Events;
using Keys;
using Enums;

namespace Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction<HorizontalInputParams> onInputDragged = delegate { };

        public UnityAction<bool> onInputTakenActive = delegate { };

        public UnityAction<InputHandlers> onInputHandlerChange = delegate (InputHandlers arg0) { };
    }
    
}