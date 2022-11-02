using Datas.ValueObject;
using Enums;
using Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class WeaponShopController : MonoBehaviour
    {
        [SerializeField]
        private ShopManager shopManager;

        private List<WeaponShopData> _curretWeaponData;

        internal void SetShopData(List<WeaponShopData> weaponShop)
        {
            _curretWeaponData = weaponShop;
        }

        internal bool OnSetBuyWeapon(WeaponTypes type, int _currentMoney)
        {
            if (CheckCanBuy(type, _currentMoney))
            {
                SendCurrentMoney(_curretWeaponData[(int)type].PurchasePrice);

                return BuyWeapon(type);
            }
            return false;
        }

        internal WeaponShopData OnSetUpgradeWeapon(WeaponTypes type, int _currentMoney)
        {
            if (CheckCanBuy(type, _currentMoney))
            {
                SendCurrentMoney(_curretWeaponData[(int)type].UpgradePrice);

                _curretWeaponData[(int)type].UpgradePrice += 100;

                _curretWeaponData[(int)type].WeaponLevel++;

                return _curretWeaponData[(int)type];
            }

            return _curretWeaponData[(int)type];
        }

        private bool CheckCanBuy(WeaponTypes type, int _currentScore)
        {
            if (_curretWeaponData[(int)type].UpgradePrice <= _currentScore)
            {
                return true;
            }
            return false;
        }

        private bool BuyWeapon(WeaponTypes type)
        {
            _curretWeaponData[(int)type].WeaponHasSold = true;

            return _curretWeaponData[(int)type].WeaponHasSold;
        }

        private void SendCurrentMoney(int _currentMoney)
        {
            shopManager.SendScoreToWeaponShop(_currentMoney);
        }
    }
}