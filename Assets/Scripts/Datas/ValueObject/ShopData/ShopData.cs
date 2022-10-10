using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Datas.ValueObject
{
    [Serializable]
    public class ShopData
    {
        public List<WeaponShopData> _weaponShopSlot = new List<WeaponShopData>();
        public List<WorkerShopData> _workerShopSlot = new List<WorkerShopData>();
        public List<PlayerShopData> _playerShopSlot = new List<PlayerShopData>();
        public List<SoldierShopData> soldierShopData = new List<SoldierShopData>();
       
    }
}