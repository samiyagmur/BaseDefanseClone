using Signals;
using System.Collections;
using TMPro;
using UnityEngine;
using Enums;
using Controllers;
using System;
using System.Collections.Generic;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
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

        #endregion

        #endregion

        #region EventSubscription
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            UISignals.Instance.onGetShopTypeOnEnter += OnOpenUIPanel;
            UISignals.Instance.onGetShopTypeOnExit += OnCloseUIPanel;
            CoreGameSignals.Instance.onUpdateGemScore += OnUpdateGemScore;
            CoreGameSignals.Instance.onUpdateMoneyScore += OnUpdateMoneyScore;

        }
        private void UnsubscribeEvents()
        {
            UISignals.Instance.onGetShopTypeOnEnter -= OnOpenUIPanel;
            UISignals.Instance.onGetShopTypeOnExit -= OnCloseUIPanel;
            CoreGameSignals.Instance.onUpdateMoneyScore -= OnUpdateMoneyScore;
            CoreGameSignals.Instance.onUpdateGemScore -= OnUpdateGemScore;
           
     
        }
        private void OnDisable() => UnsubscribeEvents();

        #region Button
        public void OnOpenUIPanel(ShopType panels) => 
            uIPanelController.OpenPanel(panels);

        public void OnCloseUIPanel(ShopType panels) =>
            uIPanelController.ClosePanel(panels);

        public void OnUpdateMoneyScore(int value)
        {
            
            levelPanelText[(int)LevelPanelTextType.money].text = value.ToString();
        }

        public void OnUpdateGemScore(int value) => levelPanelText[(int)LevelPanelTextType.diamond].text = value.ToString();

        public void ChangeWeaponType(WeaponTypes weaponTypes) =>
            UISignals.Instance.onChangeWeaponType?.Invoke(weaponTypes);//change weapon


        public void BuyWeaponButton(int weaponline)
        {
            Debug.Log(UISignals.Instance.onPressUnlockButton.Invoke((WeaponTypes)weaponline));

            buttonObject[weaponline].SetActive(UISignals.Instance.onPressUnlockButton.Invoke((WeaponTypes)weaponline));//WeaponType will Activate; 
        }

        public void UpgradeWeaponButton(int weaponline)//weaponType will Upgarde
        {
            weaponPriceText[weaponline].text =UISignals.Instance.onPressUpgradeButton.Invoke((WeaponTypes)weaponline).WeaponPrice.ToString();
            weaponLevelText[weaponline].text = "LEVEL " + UISignals.Instance.onPressUpgradeButton.Invoke((WeaponTypes)weaponline).WeaponLevel.ToString();
        }

        public void UpgradeWorkerButton(int workerButtonline)//workerType will Upgarde
        {
            workerPriceTexts[workerButtonline].text =UISignals.Instance.onPressWorkersUpgradeButtons.Invoke((WorkerUpgradeType)workerButtonline).UpgradePrice.ToString();
            workerLevelTexts[workerButtonline].text = "LEVEL " + UISignals.Instance.onPressWorkersUpgradeButtons.Invoke((WorkerUpgradeType)workerButtonline).UpgradeLevel.ToString();
        }

        public void UpgradePlayerButton(int playerButtonline)//playerType will Upgarde
        {
            playerPriceTexts[playerButtonline].text = UISignals.Instance.onPressPlayerUpgradeButtons.Invoke((PlayerUpgradeType)playerButtonline).UpgradePrice.ToString();
            playerLevelTexts[playerButtonline].text = "LEVEL " + UISignals.Instance.onPressPlayerUpgradeButtons.Invoke((PlayerUpgradeType)playerButtonline).UpgradeLevel.ToString();
        }

        public void UpgradeSoldierButton(int soldierButtonLine)//soldierType will Upgarde
        {
            soldierPriceText.text = UISignals.Instance.onPressSoldierUpgradeButton.Invoke((SoldierUpgradeType)soldierButtonLine).UpgradePrice.ToString();
            soldierLevelText.text = "LEVEL " + UISignals.Instance.onPressSoldierUpgradeButton.Invoke((SoldierUpgradeType)soldierButtonLine).UpgradeLevel.ToString();
        }
        #endregion

        #endregion


    }
}