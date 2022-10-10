using System;
using System.Collections;
using UnityEngine;
using Enums;

namespace Datas.ValueObject
{
    [Serializable]
    public class SoldierShopData 
    {
        public SoldierUpgradeType SoldierUpgradeType;
        public int UpgradePrice;
        public int UpgradeLevel;
    }
}