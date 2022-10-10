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
        private List<TextMeshPro> levelPanelText;
        [SerializeField]
        private List<TextMeshPro> weaponPriceText;
        [SerializeField]
        private List<GameObject> buttonObject;
        [SerializeField]
        private List<TextMeshPro> WorkerPriceTexts;
        [SerializeField]
        private List<TextMeshPro> PlayerPriceTexts;
        [SerializeField]
        private TextMeshPro SoldierText;

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
            CoreGameSignals.Instance.onUpdateGemScore -= OnUpdateGemScore;
            CoreGameSignals.Instance.onUpdateGemScore -= OnUpdateMoneyScore;
     
        }
        private void OnDisable() => UnsubscribeEvents();

        private void OnOpenUIPanel(ShopType panels) =>
            uIPanelController.OpenPanel(panels);

        private void OnCloseUIPanel(ShopType panels) =>
            uIPanelController.ClosePanel(panels);

        private void OnUpdateMoneyScore(int value) =>
            levelPanelText[(int)LevelPanelTextType.money].text = value.ToString();

        private void OnUpdateGemScore(int value) =>
            levelPanelText[(int)LevelPanelTextType.diamond].text = value.ToString();

        private void ChangeWeaponType(WeaponTypes weaponTypes) => 
            UISignals.Instance.onChangeWeaponType?.Invoke(weaponTypes);

        private void UpgradeWeaponButton(WeaponTypes type) =>
            weaponPriceText[(int)type].text = UISignals.Instance.onPressUpgradeButton.Invoke(type).ToString();

        private void BuyWeaponButton(WeaponTypes type) => 
            buttonObject[(int)type].SetActive(UISignals.Instance.onPressUnlockButton.Invoke(type));

        private void UpgradeWorkerButton(WorkerUpgradeType type)=> 
            WorkerPriceTexts[(int)type].text = UISignals.Instance.onPressWorkersUpgradeButtons.Invoke(type).ToString();

        private void UpgradePlayerButton(PlayerUpgradeType type) =>
            PlayerPriceTexts[(int)type].text = UISignals.Instance.onPressPlayerUpgradeButtons.Invoke(type).ToString();
        private void UpgradeSoldierButton(SoldierUpgradeType type) =>
            SoldierText.text = UISignals.Instance.onPressSoldierUpgradeButton.Invoke(type).ToString();

        #endregion


    }
}