using Data.ValueObject;
using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;
using Managers;
using Datas.ValueObject;

namespace Controllers
{
    public class WeaponShopController : MonoBehaviour
    {
        [SerializeField]
        private ShopManager shopManager;

        List<WeaponShopData> _curretWeaponData;

        internal void SetShopData(List<WeaponShopData> weaponShop)
        {
            _curretWeaponData = weaponShop;
        }

        internal bool OnSetBuyWeapon(WeaponTypes type, int _currentMoney)
        {
            
            if (CheckCanBuy(type, _currentMoney))
            {
                SendCurrentMoney(_curretWeaponData[(int)type].WeaponPrice);

                return BuyWeapon(type);
            }
          return false;
  
        }


        internal WeaponShopData OnSetUpgradeWeapon(WeaponTypes type, int _currentMoney)
        {   
            if (CheckCanBuy(type, _currentMoney))
            {   
                SendCurrentMoney(_curretWeaponData[(int)type].WeaponPrice);

                _curretWeaponData[(int)type].WeaponPrice += 100;

                _curretWeaponData[(int)type].WeaponLevel++;

                return _curretWeaponData[(int)type];
            }

            return _curretWeaponData[(int)type];
        }


        private bool CheckCanBuy(WeaponTypes type, int _currentScore)
        {
            if (_curretWeaponData[(int)type].WeaponPrice <= _currentScore)
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
