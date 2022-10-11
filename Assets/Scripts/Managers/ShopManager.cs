using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using System;
using System.Collections;
using System.Threading.Tasks;
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

        private ShopData _shopdata;
        private int _currentMoney;


        public void SaveLevelID()
        {
            InitializeDataSignals.Instance.onSaveShopData?.Invoke(_shopdata);
        }
        private void LoadShopData(ShopData shopdata)
        {
            weaponShopController.SetShopData(shopdata._weaponShopSlot);
            workerShopController.SetShopData(shopdata._workerShopSlot);
            playerShopController.SetShopData(shopdata._playerShopSlot);
            soldierShopController.SetShopData(shopdata.soldierShopData);
        }

        private void OnEnable() => SubscribeEvents();



        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
            UISignals.Instance.onPressUpgradeButton += onPressUpgradeWeapon;
            UISignals.Instance.onPressUnlockButton += onPressUnlockWeapon;
            UISignals.Instance.onPressWorkersUpgradeButtons += OnPressWorkersUpgradeButtons;
            UISignals.Instance.onPressPlayerUpgradeButtons += OnPressPlayerUpgradeButtons;
            UISignals.Instance.onPressSoldierUpgradeButton += OnPressSoldierUpgradeButton;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
            UISignals.Instance.onPressUpgradeButton -= onPressUpgradeWeapon;
            UISignals.Instance.onPressUnlockButton -= onPressUnlockWeapon;
            UISignals.Instance.onPressWorkersUpgradeButtons -= OnPressWorkersUpgradeButtons;
            UISignals.Instance.onPressPlayerUpgradeButtons -= OnPressPlayerUpgradeButtons;
            UISignals.Instance.onPressSoldierUpgradeButton -= OnPressSoldierUpgradeButton;
        }
        private void OnDisable() => UnsubscribeEvents();

        private  void OnLevelInitialize()
        {
            _shopdata = InitializeDataSignals.Instance.onLoadShopData?.Invoke();

            LoadShopData(_shopdata);
        }

        internal void GetScore() => 
            _currentMoney = CoreGameSignals.Instance.onGetCurrentMoney.Invoke();

        internal void SendScoreToWeaponShop(int _currentMoney) => 
            CoreGameSignals.Instance.onUpdateMoneyScore.Invoke(_currentMoney);

        internal void IsEnterShopsForType(ShopType shopType) =>
            UISignals.Instance.onGetShopTypeOnEnter?.Invoke(shopType);

        internal void IsExitAnyShops(ShopType shopType) =>
            UISignals.Instance.onGetShopTypeOnExit?.Invoke(shopType);

        private bool onPressUnlockWeapon(WeaponTypes type) =>
            weaponShopController.OnSetBuyWeapon(type, _currentMoney);

        private WeaponShopData onPressUpgradeWeapon(WeaponTypes type) => 
            weaponShopController.OnSetUpgradeWeapon(type, _currentMoney);

        private WorkerShopData OnPressWorkersUpgradeButtons(WorkerUpgradeType value) => 
            workerShopController.OnSetUpgradeFeature(value, _currentMoney);

        private PlayerShopData OnPressPlayerUpgradeButtons(PlayerUpgradeType value) => 
            playerShopController.OnSetUpgradeFeature(value, _currentMoney);

        private SoldierShopData OnPressSoldierUpgradeButton(SoldierUpgradeType value) => 
            soldierShopController.OnSetUpgradeFeature(value, _currentMoney);

     






    }
}