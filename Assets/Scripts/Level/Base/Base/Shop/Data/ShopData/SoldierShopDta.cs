using System;
using System.Collections;
using UnityEngine;
using Enums;
using Sirenix.OdinInspector;

namespace Datas.ValueObject
{
    [Serializable]
    public class SoldierShopData 
    {
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Right")]
        [PreviewField(80)]
        public Sprite Image;

        [VerticalGroup("Split/Left")]
        public string Name;

        [VerticalGroup("Split/Left")]
        public int UpgradePrice;

        [VerticalGroup("Split/Left")]
        [ReadOnly]
        public int UpgradeLevel;
    }
}