using Datas.ValueObject;
using Enums;
using Managers;
using Signals;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Controllers
{
    public class WorkerShopController : MonoBehaviour
    {
        [SerializeField]
        private ShopManager shopManager;

        private List<WorkerShopData> _workerShopData;
        [SerializeField]
  
        internal void SetShopData(List<WorkerShopData> workerShop) => _workerShopData = workerShop;


        internal WorkerShopData OnSetUpgradeFeature(WorkerUpgradeType value, int _currentMoney)
        {
            if (_workerShopData[(int)value].UpgradePrice <= _currentMoney)
            {
                SendCurrentMoney(_workerShopData[(int)value].UpgradePrice);

                _workerShopData[(int)value].UpgradePrice+=100;

                _workerShopData[(int)value].UpgradeLevel++;
                IncreaseFetures(value);


                return _workerShopData[(int)value];
            }
            return _workerShopData[(int)value];
        }

        private void IncreaseFetures(WorkerUpgradeType value)
        {
          
            switch (value)  
            {
                case WorkerUpgradeType.Capasity:
                    AmmoManagerSignals.Instance.onIncreaseAmmoWorkerCapasity?.Invoke(2);
                    break;
                case WorkerUpgradeType.Speed:
                    AmmoManagerSignals.Instance.onIncreaseAmmoWorkerSpeed?.Invoke(0.4f);
                    break;
                default:
                    break;
            }
        }

        private void SendCurrentMoney(int _currentMoney)
        {
            shopManager.SendScoreToWeaponShop(_currentMoney);
        }
    }
}
