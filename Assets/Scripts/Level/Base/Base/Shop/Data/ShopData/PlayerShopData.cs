using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class PlayerShopData
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