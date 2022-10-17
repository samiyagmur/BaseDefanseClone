using System;

namespace Data.ValueObject
{
    [Serializable]
    public class StageData
    {
        public bool Unlocked;
        public int StageCost;
        public int StagePayedAmount;
    }
}