using Datas.ValueObject;
using Enums;
using Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class SoldierShopController : MonoBehaviour
    {
        [SerializeField]
        private ShopManager shopManager;

        private List<SoldierShopData> _soldierShopData;

        internal void SetShopData(List<SoldierShopData> soldierShopData) => _soldierShopData = soldierShopData;

        internal SoldierShopData OnSetUpgradeFeature(SoldierUpgradeType value, int _currentMoney)
        {
            if (_soldierShopData[(int)value].UpgradeLevel <= _currentMoney)
            {
                SendCurrentMoney(_soldierShopData[0].UpgradePrice);

                _soldierShopData[(int)value].UpgradePrice += 100;

                _soldierShopData[(int)value].UpgradeLevel++;

                return _soldierShopData[(int)value];
            }
            return _soldierShopData[(int)value];
        }

        private void SendCurrentMoney(int _currentMoney)
        {
            shopManager.SendScoreToWeaponShop(_currentMoney);
        }
    }
}