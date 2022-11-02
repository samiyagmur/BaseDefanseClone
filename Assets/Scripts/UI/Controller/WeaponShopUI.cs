using Datas.ValueObject;
using Enums;
using Managers;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class WeaponShopUI : MonoBehaviour
    {
        [SerializeField]
        private WeaponTypes weaponType;

        [SerializeField]
        private Image image;

        [SerializeField]
        private TextMeshProUGUI purchasePrice;

        [SerializeField]
        private TextMeshProUGUI key;

        [SerializeField]
        private TextMeshProUGUI upgradePrice;

        [SerializeField]
        private TextMeshProUGUI weaponLevel;

        [SerializeField]
        private Button selectWeaponType;

        [SerializeField]
        private Button buyWeaponButton;

        [SerializeField]
        private Button upgradeWeaponButton;

        private WeaponShopData _weaponShopSlot;

        private UIManager _uIManager;

        private void Start()
        {
            InitButton();
        }

        private void InitButton()
        {
            selectWeaponType.onClick.AddListener(ChangeWeaponButton);
            buyWeaponButton.onClick.AddListener(BuyWeaponButton);
            upgradeWeaponButton.onClick.AddListener(UpgradeWeaponButton);
        }

        internal void SetToShopData(List<WeaponShopData> weaponShopSlot, UIManager uIManager)
        {
            _weaponShopSlot = weaponShopSlot[(int)weaponType];
            _uIManager = uIManager;
            InitWeapon();
        }

        private void InitWeapon()
        {
            upgradePrice.text = _weaponShopSlot.UpgradePrice.ToString();
            weaponLevel.text = "LEVEL " + _weaponShopSlot.WeaponLevel.ToString();
            key.text = _weaponShopSlot.Name.ToUpper();
            gameObject.name = _weaponShopSlot.Name;
            image.sprite = _weaponShopSlot.Image;
            purchasePrice.text = _weaponShopSlot.PurchasePrice.ToString();

            buyWeaponButton.gameObject.SetActive(!_weaponShopSlot.WeaponHasSold);
            selectWeaponType.gameObject.SetActive(_weaponShopSlot.WeaponHasSold);
            upgradeWeaponButton.gameObject.SetActive(_weaponShopSlot.WeaponHasSold);
        }

        private void ChangeWeaponButton()
        {
            _uIManager.ChangeWeaponType(weaponType);
        }

        private void BuyWeaponButton()
        {
            _uIManager.BuyWeapon(weaponType);

            InitWeapon();
        }

        private void UpgradeWeaponButton()
        {
            _uIManager.UpgradeWeapon(weaponType);

            InitWeapon();
        }

        public void SetWeaponType(WeaponTypes weaponTypes)
        {
            weaponType = weaponTypes;
        }
    }
}