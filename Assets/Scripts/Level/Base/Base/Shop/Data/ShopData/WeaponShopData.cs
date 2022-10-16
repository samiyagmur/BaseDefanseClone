﻿using Enums;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Datas.ValueObject
{
    [Serializable]
    public class WeaponShopData 
    {
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Right")]
        [PreviewField(80)]
        public Sprite Image;

        [VerticalGroup("Split/Left")]
        public string Name;
        [VerticalGroup("Split/Left")]
        public int WeaponPrice;
        [VerticalGroup("Split/Left")]
        public bool WeaponHasSold;

        [VerticalGroup("Split/Left")]
        [ReadOnly]
        public int WeaponLevel;

        


        

       

    }
}