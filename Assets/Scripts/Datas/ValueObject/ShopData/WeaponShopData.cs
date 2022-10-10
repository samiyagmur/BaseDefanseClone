using Enums;
using System;
using System.Collections;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class WeaponShopData 
    {
        public WeaponTypes weaponType;
        public int WeaponPrice;
        public bool WeaponHasSold;
        public int WeaponLevel;

    }
}