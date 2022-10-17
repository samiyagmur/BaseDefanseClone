using Datas.ValueObject;
using System;

namespace Data.ValueObject
{
    [Serializable]
    public class RoomData
    { 
        public TurretData TurretData;
        public int RoomCost;
        public int RoomPayedAmount;
    }
}