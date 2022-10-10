using Controllers;
using Enums;
using Signals;
using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField]
        private WeaponShopController weaponShopController;
        [SerializeField]
        private WorkerShopController workerShopController;
        [SerializeField]
        private PlayerShopController playerShopController;
        [SerializeField]
        private SoldierShopController soldierShopController;
        private int _currentMoney;

        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            UISignals.Instance.onPressUpgradeButton += onPressUpgradeWeapon;
            UISignals.Instance.onPressUnlockButton += onPressUnlockWeapon;
            UISignals.Instance.onPressWorkersUpgradeButtons += OnPressWorkersUpgradeButtons;
            UISignals.Instance.onPressPlayerUpgradeButtons += OnPressPlayerUpgradeButtons;
            UISignals.Instance.onPressSoldierUpgradeButton += OnPressSoldierUpgradeButton;



        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onPressUpgradeButton -= onPressUpgradeWeapon;
            UISignals.Instance.onPressUnlockButton -= onPressUnlockWeapon;

        }
        private void OnDisable() => UnsubscribeEvents();

        internal void GetScore() => _currentMoney = CoreGameSignals.Instance.onGetCurrentMoney.Invoke();


        internal void IsEnterShopsForType(ShopType shopType) => UISignals.Instance.onGetShopTypeOnEnter?.Invoke(shopType);

        internal void IsExitAnyShops(ShopType shopType) => UISignals.Instance.onGetShopTypeOnExit?.Invoke(shopType);


        private int onPressUpgradeWeapon(WeaponTypes type) => weaponShopController.OnSetUpgradeWeapon(type,_currentMoney);

        private bool onPressUnlockWeapon(WeaponTypes type) => weaponShopController.OnSetBuyWeapon(type, _currentMoney);

        private int OnPressWorkersUpgradeButtons(WorkerUpgradeType value)
        {
            return workerShopController.OnSetUpgradeFeature(value, _currentMoney);
        }

        private int OnPressPlayerUpgradeButtons(PlayerUpgradeType value)
        {
            return playerShopController.OnSetUpgradeFeature(value, _currentMoney);
        }

        private int OnPressSoldierUpgradeButton(SoldierUpgradeType value)
        {
            return soldierShopController.OnSetUpgradeFeature(value, _currentMoney);
        }

       








    }
}