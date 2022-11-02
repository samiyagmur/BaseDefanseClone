using System;
using System.Collections.Generic;

namespace Data.ValueObject
{
    [Serializable]
    public class BaseRoomData
    {
        public List<RoomData> Rooms = new List<RoomData>();
    }
}