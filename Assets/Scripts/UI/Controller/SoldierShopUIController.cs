using Datas.ValueObject;
using Enums;
using Managers;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class SoldierShopUIController : MonoBehaviour
    {
        [SerializeField]
        private SoldierUpgradeType soldierUpgradeType;

        [SerializeField]
        private Image image;

        [SerializeField]
        private TextMeshProUGUI ket;

        [SerializeField]
        private TextMeshProUGUI feturesPrice;

        [SerializeField]
        private TextMeshProUGUI feturesLevel;

        [SerializeField]
        private Button upgradeSoldierButton;

        private SoldierShopData _soldierShopData;

        private UIManager _uIManager;

        private void Start() => InitButton();

        private void InitButton() => upgradeSoldierButton.onClick.AddListener(UpgradeSoldierButton);

        internal void SetToShopData(List<SoldierShopData> soldierShopSlot, UIManager uIManager)
        {
            _soldierShopData = soldierShopSlot[(int)soldierUpgradeType];
            _uIManager = uIManager;
            InitFertures();
        }

        private void InitFertures()
        {
            feturesPrice.text = _soldierShopData.UpgradePrice.ToString();
            feturesLevel.text = "LEVEL " + _soldierShopData.UpgradeLevel.ToString();
            ket.text = _soldierShopData.Name.ToUpper();
            gameObject.name = _soldierShopData.Name;
            image.sprite = _soldierShopData.Image;
        }

        private void UpgradeSoldierButton()
        {
            _uIManager.UpgradeSoldierButton(soldierUpgradeType);

            feturesPrice.text = _soldierShopData.UpgradePrice.ToString();
            feturesLevel.text = "LEVEL " + _soldierShopData.UpgradeLevel.ToString();
        }

        internal void SetWeaponType(SoldierUpgradeType soldierUpgradeTypes)
        {
            soldierUpgradeType = soldierUpgradeTypes;
        }
    }
}