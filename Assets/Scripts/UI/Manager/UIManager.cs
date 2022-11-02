using Controller;
using Controllers;
using Datas.ValueObject;
using Enums;
using Interfaces;
using Signals;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour, IGetPoolObject
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField]
        private Transform weponShopHolder;

        [SerializeField]
        private Transform workerShopHolder;

        [SerializeField]
        private Transform playerShopHolder;

        [SerializeField]
        private Transform soldierShopHolder;

        [SerializeField]
        private UIPanelController uIPanelController;

        [SerializeField]
        private List<TextMeshProUGUI> levelPanelText;

        #endregion Serialized Variables

        #region Private Variables

        private ShopData _shopdata;

        private WeaponShopUI _weaponShopUI;

        private WorkerSopUIController _workerSopUIController;

        private PlayerShopUIController _playerShopUIController;

        private SoldierShopUIController _soldierShopUIController;

        #endregion Private Variables

        #endregion Self Variables

        #region EventSubscription

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenUIPanel += OnOpenUIPanel;
            UISignals.Instance.onCloseUIPanel += OnCloseUIPanel;
            UISignals.Instance.onChangeMoney += OnChangeMoney;
            UISignals.Instance.onChangeDiamond += OnChangeDiamond;
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenUIPanel -= OnOpenUIPanel;
            UISignals.Instance.onCloseUIPanel -= OnCloseUIPanel;
            UISignals.Instance.onChangeMoney -= OnChangeMoney;
            UISignals.Instance.onChangeDiamond -= OnChangeDiamond;
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void OnLevelInitialize()
        {
            _shopdata = InitializeDataSignals.Instance.onLoadShopData?.Invoke();

            LoadWeaponSlot();

            LoadWorkerSlot();

            LoadPlayerSlot();

            LoadSoldierSlot();
        }

        internal void LoadWeaponSlot()
        {
            for (int i = 0; i < _shopdata._weaponShopSlot.Count; i++)
            {
                _weaponShopUI = GetObject(PoolType.WeaponPanel).GetComponent<WeaponShopUI>();

                _weaponShopUI.transform.SetParent(weponShopHolder);

                _weaponShopUI.SetWeaponType((WeaponTypes)i);

                _weaponShopUI.SetToShopData(_shopdata._weaponShopSlot, this);
            }
        }

        internal void LoadWorkerSlot()
        {
            for (int i = 0; i < _shopdata._workerShopSlot.Count; i++)
            {
                _workerSopUIController = GetObject(PoolType.NewWorkerFeturesSlot).GetComponent<WorkerSopUIController>();

                _workerSopUIController.transform.SetParent(workerShopHolder);

                _workerSopUIController.SetWeaponType((WorkerUpgradeType)i);

                _workerSopUIController.SetToShopData(_shopdata._workerShopSlot, this);
            }
        }

        internal void LoadPlayerSlot()
        {
            for (int i = 0; i < _shopdata._playerShopSlot.Count; i++)
            {
                _playerShopUIController = GetObject(PoolType.NewPlayerFeturesSlot).GetComponent<PlayerShopUIController>();

                _playerShopUIController.transform.SetParent(playerShopHolder);

                _playerShopUIController.SetWeaponType((PlayerUpgradeType)i);

                _playerShopUIController.SetToShopData(_shopdata._playerShopSlot, this);
            }
        }

        internal void LoadSoldierSlot()
        {
            for (int i = 0; i < _shopdata.soldierShopData.Count; i++)
            {
                _soldierShopUIController = GetObject(PoolType.NewSoldierrFeturesSlot).GetComponent<SoldierShopUIController>();

                _soldierShopUIController.transform.SetParent(soldierShopHolder);

                _soldierShopUIController.SetWeaponType((SoldierUpgradeType)i);

                _soldierShopUIController.SetToShopData(_shopdata.soldierShopData, this);
            }
        }

        #region Button

        public void OnOpenUIPanel(UIPanels panels) =>
            uIPanelController.OpenPanel(panels);

        public void OnCloseUIPanel(UIPanels panels) =>
            uIPanelController.ClosePanel(panels);

        public void ChangeWeaponType(WeaponTypes weapontype) =>
            UISignals.Instance.onChangeWeaponType?.Invoke(weapontype);

        private void OnChangeMoney(int amount) =>
           levelPanelText[(int)LevelPanelTextType.money].text = amount.ToString();

        private void OnChangeDiamond(int amount) =>
           levelPanelText[(int)LevelPanelTextType.diamond].text = amount.ToString();

        public void BuyWeapon(WeaponTypes weapontype) =>
            UISignals.Instance.onPressUnlockButton.Invoke(weapontype);

        public void UpgradeWeapon(WeaponTypes weaponType) =>
            UISignals.Instance.onPressUpgradeButton.Invoke(weaponType);

        public void UpgradeWorkerButton(WorkerUpgradeType workerUpgradeType) =>
            UISignals.Instance.onPressWorkersUpgradeButtons.Invoke(workerUpgradeType);

        public void UpgradePlayerButton(PlayerUpgradeType playerUpgradeType) =>
            UISignals.Instance.onPressPlayerUpgradeButtons.Invoke(playerUpgradeType);

        public void UpgradeSoldierButton(SoldierUpgradeType soldierUpgradeType) =>
            UISignals.Instance.onPressSoldierUpgradeButton.Invoke(soldierUpgradeType);

        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        }

        #endregion Button

        #endregion EventSubscription
    }
}