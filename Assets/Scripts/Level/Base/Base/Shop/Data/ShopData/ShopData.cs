using Enums;
using Sirenix.OdinInspector;
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
        
        [BoxGroup("WeaponShop")]
        
        [ListDrawerSettings(HideAddButton =true,HideRemoveButton =true)]
        public List<WeaponShopData> _weaponShopSlot = new List<WeaponShopData>();

        [HorizontalGroup("WeaponShop/Split")]
        [VerticalGroup("WeaponShop/Split/Left")]
        [Button("AddWeaponSlot")]
        private void AddNewWeapon()
        {
            _weaponShopSlot.Add(new WeaponShopData());
        }

   
        [VerticalGroup("WeaponShop/Split/Right")]
        [Button("RemoveWeaponSlot")]
        private void RemoveNewWeapon()
        {
            _weaponShopSlot.RemoveAt(_weaponShopSlot.Count - 1);
            _weaponShopSlot.TrimExcess();
        }

        [Space(5)]
        [BoxGroup("WorkerShop")]
        [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
        public List<WorkerShopData> _workerShopSlot = new List<WorkerShopData>();

        [HorizontalGroup("WorkerShop/Split")]
        [VerticalGroup("WorkerShop/Split/Left")]
        [Button("AddNewWorkersFeatureSlot")]
        private void AddNewWorkerFeature()
        {
            _workerShopSlot.Add(new WorkerShopData());
            _weaponShopSlot.TrimExcess();
        }

        [VerticalGroup("WorkerShop/Split/Right")]
        [Button("RemoveWorkersFeatureSlot")]
        private void RemoveNewWorkerFeature()
        {
            _workerShopSlot.RemoveAt(_workerShopSlot.Count - 1);
            _weaponShopSlot.TrimExcess();
        }

        [Space(5)]
        [BoxGroup("PlayerShop")]
        [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
        public List<PlayerShopData> _playerShopSlot = new List<PlayerShopData>();

        [HorizontalGroup("PlayerShop/Split")]
        [VerticalGroup("PlayerShop/Split/Left")]
        [Button("AddNewPlayerFeatureSlot")]
        private void AddNewPlayerFeature()
        {
            _playerShopSlot.Add(new PlayerShopData());
        }

        [VerticalGroup("PlayerShop/Split/Right")]
        [Button("RemovePlayerFeatureSlot")]
        private void RemovePlayerFeature()
        {
            _playerShopSlot.RemoveAt(_playerShopSlot.Count - 1);
            _weaponShopSlot.TrimExcess();
        }

        [Space(5)]
        [BoxGroup("SoliderShop")]
        [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
        public List<SoldierShopData> soldierShopData = new List<SoldierShopData>();

        [HorizontalGroup("SoliderShop/Split")]
        [VerticalGroup("SoliderShop/Split/Left")]
        [Button("AddNewSoliderFeatureSlot")]
        private void AddNewSoldierFeature()
        {
            soldierShopData.Add(new SoldierShopData());
        }

        [VerticalGroup("SoliderShop/Split/Right")]
        [Button("RemoveSoliderFeatureSlot")]
        private void RemoveSoldierFeature()
        {
            soldierShopData.RemoveAt(soldierShopData.Count-1);
        }





    }
}