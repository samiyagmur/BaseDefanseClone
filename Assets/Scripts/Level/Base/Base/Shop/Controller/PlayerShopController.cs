using Datas.ValueObject;
using Enums;
using Managers;
using Signals;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class PlayerShopController : MonoBehaviour
    {
        [SerializeField]
        private ShopManager shopManager;

        private List<PlayerShopData> _playerShopData;
        private int _increasedSpeedCost = +10;

        internal void SetShopData(List<PlayerShopData> playerShop) => _playerShopData = playerShop;

        internal PlayerShopData OnSetUpgradeFeature(PlayerUpgradeType value, int _currentMoney)
        {
            if (_playerShopData[(int)value].UpgradePrice <= _currentMoney)
            {
                SendCurrentMoney(_playerShopData[(int)value].UpgradePrice);

                _playerShopData[(int)value].UpgradePrice += 100;

                _playerShopData[(int)value].UpgradeLevel++;
                UpdateFeateru(value);

                return _playerShopData[(int)value];
            }
            return _playerShopData[(int)value];
        }

        private void UpdateFeateru(PlayerUpgradeType playerUpgradeType)
        {
            switch (playerUpgradeType)
            {
                case PlayerUpgradeType.Capasity:
                    break;

                case PlayerUpgradeType.Speed:
                    break;

                case PlayerUpgradeType.Healt:
                    PlayerSignal.Instance.onIncreaseHealt?.Invoke(_increasedSpeedCost);
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