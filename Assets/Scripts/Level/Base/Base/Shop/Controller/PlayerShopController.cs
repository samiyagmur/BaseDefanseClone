using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Managers;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Controllers
{
    public class PlayerShopController : MonoBehaviour
    {
        [SerializeField]
        private ShopManager shopManager;

        private List<PlayerShopData> _playerShopData;
        private int _increasedSpeedCost= +10;

        internal void SetShopData(List<PlayerShopData> playerShop) => _playerShopData = playerShop;

        internal PlayerShopData OnSetUpgradeFeature(PlayerUpgradeType value, int _currentMoney)
        {
           

                if (_playerShopData[(int)value].UpgradePrice <= _currentMoney)
                {
                    SendCurrentMoney(_playerShopData[(int)value].UpgradePrice);

                    _playerShopData[(int)value].UpgradePrice += 100;
    
                    _playerShopData[(int)value].UpgradeLevel++;

                    PlayerSignal.Instance.onIncreaseHealt?.Invoke(_increasedSpeedCost);

                    return _playerShopData[(int)value];
                }
                return _playerShopData[(int)value];
            
        }

        private void SendCurrentMoney(int _currentMoney)
        {
            shopManager.SendScoreToWeaponShop(_currentMoney);
        }
    }
}