using Enums;
using System;
using System.Collections;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class PlayerShopData 
    {

        public PlayerUpgradeType PlayerUpgradeType;
        public int UpgradePrice;
        public int UpgradeLevel;
    }
}