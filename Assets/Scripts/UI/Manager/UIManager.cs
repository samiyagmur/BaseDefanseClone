using Signals;
using System.Collections;
using TMPro;
using UnityEngine;
using Enums;
using Controllers;
using System;
using System.Collections.Generic;
using Datas.ValueObject;
using Controller;
using System.Threading.Tasks;
using Interfaces;

namespace Managers
{
    public class UIManager : MonoBehaviour,IGetPoolObject
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


        #endregion

        #region Private Variables

        private ShopData _shopdata;

        private WeaponShopUI weaponShopUI;

        private WorkerSopUIController workerSopUIController;

        private PlayerShopUIController playerShopUIController;

        private SoldierShopUIController soldierShopUIController;

        #endregion

        #endregion

        #region EventSubscription
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            UISignals.Instance.onGetShopTypeOnEnter += OnOpenUIPanel;
            UISignals.Instance.onGetShopTypeOnExit += OnCloseUIPanel;
            UISignals.Instance.onChangeMoney += OnChangeMoney;
            UISignals.Instance.onChangeDiamond += OnChangeDiamond;
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;

        }

      

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onGetShopTypeOnEnter -= OnOpenUIPanel;
            UISignals.Instance.onGetShopTypeOnExit -= OnCloseUIPanel;
            UISignals.Instance.onChangeMoney -= OnChangeMoney;
            UISignals.Instance.onChangeDiamond -= OnChangeDiamond;
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
        }

        private void OnDisable()
        {

            UnsubscribeEvents();
        }

        private void OnLevelInitialize()
        {
            
            _shopdata = InitializeDataSignals.Instance.onLoadShopData?.Invoke();
            

            LoadWeaponSlot();

            LoadWorkerSlot();

            LoadPlayerSlot();

            LoadSoldierSlot();


        }

        internal  void LoadWeaponSlot()
        {

            for (int i = 0; i < _shopdata._weaponShopSlot.Count; i++)
            {

                weaponShopUI = GetObject(PoolType.WeaponPanel).GetComponent<WeaponShopUI>();

                weaponShopUI.transform.SetParent(weponShopHolder);

                weaponShopUI.SetWeaponType((WeaponTypes)i);

                weaponShopUI.SetToShopData(_shopdata._weaponShopSlot,this);

               

            }

        }

        internal void LoadWorkerSlot()
        {

            for (int i = 0; i < _shopdata._workerShopSlot.Count; i++)
            {

                workerSopUIController = GetObject(PoolType.NewWorkerFeturesSlot).GetComponent<WorkerSopUIController>();

                workerSopUIController.transform.SetParent(workerShopHolder);

                workerSopUIController.SetWeaponType((WorkerUpgradeType)i);

                workerSopUIController.SetToShopData(_shopdata._workerShopSlot, this);

               
            }

        }
        internal void LoadPlayerSlot()
        {

            for (int i = 0; i < _shopdata._playerShopSlot.Count; i++)
            {

                playerShopUIController = GetObject(PoolType.NewPlayerFeturesSlot).GetComponent<PlayerShopUIController>();

                playerShopUIController.transform.SetParent(playerShopHolder);

                playerShopUIController.SetWeaponType((PlayerUpgradeType)i);

                playerShopUIController.SetToShopData(_shopdata._playerShopSlot, this);

               
            }

        }
        internal void LoadSoldierSlot()
        {

            for (int i = 0; i < _shopdata.soldierShopData.Count; i++)
            {

                soldierShopUIController = GetObject(PoolType.NewSoldierrFeturesSlot).GetComponent<SoldierShopUIController>();

                soldierShopUIController.transform.SetParent(soldierShopHolder);

                soldierShopUIController.SetWeaponType((SoldierUpgradeType)i);

                soldierShopUIController.SetToShopData(_shopdata.soldierShopData, this);

               
            }

        }

        #region Button
        public void OnOpenUIPanel(ShopType panels) => 
            uIPanelController.OpenPanel(panels);

        public void OnCloseUIPanel(ShopType panels) =>
            uIPanelController.ClosePanel(panels);

        public void ChangeWeaponType(WeaponTypes weapontype) =>
            UISignals.Instance.onChangeWeaponType?.Invoke(weapontype);

        private void OnChangeDiamond(int amount) => 
            levelPanelText[(int)LevelPanelTextType.diamond].text = amount.ToString();

        private void OnChangeMoney(int amount) => 
            levelPanelText[(int)LevelPanelTextType.money].text = amount.ToString();

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
        #endregion

        #endregion




    }
}