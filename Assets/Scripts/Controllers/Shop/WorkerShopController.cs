using Datas.ValueObject;
using Enums;
using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Controllers
{
    public class WorkerShopController : MonoBehaviour
    {
        private SerializedDictionary<WorkerUpgradeType, WorkerShopData> _weaponShopSlot = new SerializedDictionary<WeaponTypes, WorkerShopData>();


        internal int OnSetUpgradeFeature(WorkerUpgradeType value, int _currentMoney)
        {

      
            if (_weaponShopSlot[value].UpgradePrice <= _currentMoney)
            {
                _currentMoney = _currentMoney - _weaponShopSlot[value].UpgradePrice;


            }

        }

        

        //data setlencek

    }
}
