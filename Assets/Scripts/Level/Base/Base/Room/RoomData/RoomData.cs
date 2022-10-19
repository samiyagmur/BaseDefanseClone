using Datas.ValueObject;
using Enums;
using System;

namespace Data.ValueObject
{
    [Serializable]
    public class RoomData
    {
        public RoomTypes RoomTypes;

        public SideBaseAvaliableStatus SideBaseAvaliableStatus;

        public TurretLocationType TurretLocationType;

        public TurretData TurretData;

        public int PayedAmount;

        public int Cost;
    }
}