using Data.ValueObject;
using Enums;
using Extentions;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class BaseSignals : MonoSingleton<BaseSignals>
    {
        public UnityAction<RoomTypes> onChangeExtentionVisibility = delegate { };
        public Func<RoomTypes, RoomData> onSetRoomData;
        public UnityAction<RoomData, RoomTypes> onUpdateRoomData = delegate { };

    }
}