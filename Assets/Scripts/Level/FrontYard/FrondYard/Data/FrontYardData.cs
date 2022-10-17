using Data.ValueObjects;
using System;
using System.Collections.Generic;

namespace Data.ValueObject
{
    [Serializable]
    public class FrontYardData
    {
        public List<StageData> StageDataList;
        public List<FrontYardItemsData> FrontYardItemsDataList;
        public HostageData HostageData;
    }
}