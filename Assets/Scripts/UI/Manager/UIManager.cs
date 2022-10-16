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

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField]
        private WeaponShopUI _weaponShopUI;
        [SerializeField]
        private Transform _weponShopHolder;
        [SerializeField]
        private UIPanelController uIPanelController;
        [SerializeField]
        private List<TextMeshProUGUI> levelPanelText;
        [SerializeField]
        private List<TextMeshProUGUI> weaponPriceText;
        [SerializeField]
        private List<TextMeshProUGUI> weaponLevelText;
        [SerializeField]
        private List<GameObject> buttonObject;
        [SerializeField]
        private List<GameObject> unlockButton;
        [SerializeField]
        private List<GameObject> selectButton;
        [SerializeField]
        private List<TextMeshProUGUI> workerPriceTexts;
        [SerializeField]
        private List<TextMeshProUGUI> workerLevelTexts;
        [SerializeField]
        private List<TextMeshProUGUI> playerPriceTexts;
        [SerializeField]
        private List<TextMeshProUGUI> playerLevelTexts;
        [SerializeField]
        private TextMeshProUGUI soldierPriceText;

        [SerializeField]
        private TextMeshProUGUI soldierLevelText;

        #endregion

        #region Private Variables
        private ShopData _shopdata;
        private int _maxSlotCountInShops=4;

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


        private void OnLevelInitialize()
        {
            
            _shopdata = InitializeDataSignals.Instance.onLoadShopData?.Invoke();
            LoadWeapon();
        
        }

        internal void LoadWeapon()
        {

            for (int i = 0; i < _shopdata._weaponShopSlot.Count; i++)
            {
                _weaponShopUI.SetToShopData(_shopdata._weaponShopSlot, this);

                Instantiate(_weaponShopUI.gameObject, _weponShopHolder);
                _weaponShopUI.SetWeaponType((WeaponTypes)i); 
            }

        }



































        //private void InitText()
        //{   
        //    for (int shopSlotCount = 0; shopSlotCount < _maxSlotCountInShops; shopSlotCount++)
        //    {
        //            weaponPriceText[shopSlotCount].text = _shopdata._weaponShopSlot[shopSlotCount].WeaponPrice.ToString();
        //            weaponLevelText[shopSlotCount].text = "LEVEL " + _shopdata._weaponShopSlot[shopSlotCount].WeaponLevel.ToString();
        //            buttonObject[shopSlotCount].SetActive(_shopdata._weaponShopSlot[shopSlotCount].WeaponHasSold);
        //            selectButton[shopSlotCount].SetActive(_shopdata._weaponShopSlot[shopSlotCount].WeaponHasSold);
        //            unlockButton[shopSlotCount].SetActive(!_shopdata._weaponShopSlot[shopSlotCount].WeaponHasSold);

        //        if ((shopSlotCount < 3)){
        //            playerPriceTexts[shopSlotCount].text = _shopdata._playerShopSlot[shopSlotCount].UpgradePrice.ToString();
        //            playerLevelTexts[shopSlotCount].text = "LEVEL " + _shopdata._playerShopSlot[shopSlotCount].UpgradeLevel.ToString();

        //        }

        //        if ((shopSlotCount < 2)){
        //            workerPriceTexts[shopSlotCount].text = _shopdata._workerShopSlot[shopSlotCount].UpgradePrice.ToString();
        //            workerLevelTexts[shopSlotCount].text = "LEVEL " + _shopdata._workerShopSlot[shopSlotCount].UpgradeLevel.ToString(); 
        //        }

        //        if (shopSlotCount < 1) {

        //            soldierPriceText.text = _shopdata.soldierShopData[shopSlotCount].UpgradePrice.ToString();
        //            soldierLevelText.text = "LEVEL " + _shopdata.soldierShopData[shopSlotCount].UpgradeLevel.ToString();
        //        }
        //    }
        //}

        private void OnDisable() => UnsubscribeEvents();

        #region Button
        public void OnOpenUIPanel(ShopType panels) => 
            uIPanelController.OpenPanel(panels);

        public void OnCloseUIPanel(ShopType panels) =>
            uIPanelController.ClosePanel(panels);

        public void ChangeWeaponType(WeaponTypes weapontype) =>
            UISignals.Instance.onChangeWeaponType?.Invoke(weapontype);//change weapon


        private void OnChangeDiamond(int amount) => 
            levelPanelText[(int)LevelPanelTextType.diamond].text = amount.ToString();

        private void OnChangeMoney(int amount) => 
            levelPanelText[(int)LevelPanelTextType.money].text = amount.ToString();

        public bool BuyWeapon(WeaponTypes weapontype) =>
            UISignals.Instance.onPressUnlockButton.Invoke(weapontype);


        public WeaponShopData UpgradeWeapon(WeaponTypes weaponType) =>
            UISignals.Instance.onPressUpgradeButton.Invoke(weaponType);


















        public void UpgradeWorkerButton(int workerButtonline)//workerType will Upgarde
        {  
            WorkerShopData workerShopData = UISignals.Instance.onPressWorkersUpgradeButtons.Invoke((WorkerUpgradeType)workerButtonline);

            workerPriceTexts[workerButtonline].text = workerShopData.UpgradePrice.ToString();
            workerLevelTexts[workerButtonline].text = "LEVEL " + workerShopData.UpgradeLevel.ToString();
          
        }


        public void UpgradePlayerButton(int playerButtonline)//playerType will Upgarde
        {
            PlayerShopData playerShopData = UISignals.Instance.onPressPlayerUpgradeButtons.Invoke((PlayerUpgradeType)playerButtonline);

            playerPriceTexts[playerButtonline].text = playerShopData.UpgradePrice.ToString();
            playerLevelTexts[playerButtonline].text = "LEVEL " + playerShopData.UpgradeLevel.ToString();
          
        }

        public void UpgradeSoldierButton(int soldierButtonLine)//soldierType will Upgarde
        {
            SoldierShopData soldierShopData= UISignals.Instance.onPressSoldierUpgradeButton.Invoke((SoldierUpgradeType)soldierButtonLine);

            soldierPriceText.text = soldierShopData.UpgradePrice.ToString();
            soldierLevelText.text = "LEVEL " + soldierShopData.UpgradeLevel.ToString();
      
        }
        #endregion

        #endregion

        


    }
}