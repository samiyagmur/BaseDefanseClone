using Data.ValueObject;
using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;

namespace Controllers
{
    public class WeaponShopController : MonoBehaviour
    {
        WeaponData _curretWeaponData;

        int _buyablePrice;

        //saveden gelcek
        
        private SerializedDictionary<WeaponTypes, SerializedDictionary<int, WeaponData>> _weaponShopSlot = new SerializedDictionary<WeaponTypes, SerializedDictionary<int, WeaponData>>();
        private int _currentScore;

        internal bool OnSetBuyWeapon(WeaponTypes type, int _currentMoney)
        {
            if (CheckCanBuy(type, _currentMoney))
            {
                return BuyWeapon(type);
            }
          return false;
  
        }
        internal int OnSetUpgradeWeapon(WeaponTypes type, int _currentMoney)
        {

            if (CheckCanBuy(type, _currentMoney))
            {
               // _currentScore -= _weaponShopSlot[type].WeaponPrice;

                return _currentScore;
            }

            return _currentScore;
        }

        private bool CheckCanBuy(WeaponTypes type, int _currentScore)
        {
            //if (_weaponShopSlot[type].WeaponPrice <= _currentScore)
            //{
            //    return true;
            //}
            //return false;
        }

        private bool BuyWeapon(WeaponTypes type)
        {
            _weaponShopSlot[type].WeaponHasSold = true;

            return _weaponShopSlot[type].WeaponHasSold;
        }

        /// <summary>
        /// scoredan gelecek
        /// </summary>
        /// <param name="currentScore"></param>
        internal void SetCurrentMoneyScore(int currentScore)
        {
            _currentScore = currentScore;
        }
    } 
}
