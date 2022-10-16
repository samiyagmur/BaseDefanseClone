using Datas.ValueObject;
using Enums;
using Managers;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class WeaponShopUI : MonoBehaviour
    {
        [SerializeField] 
        WeaponTypes weaponType;
        [SerializeField]
        private Image image;
        [SerializeField]
        private TextMeshProUGUI name;
        [SerializeField]
        private TextMeshProUGUI weaponPrice;
        [SerializeField]
        private TextMeshProUGUI weaponLevel;
        [SerializeField]
        private Button changeWeaponType;
        [SerializeField]
        private Button buyWeaponButton;
        [SerializeField]
        private Button upgradeWeaponButton;


        private WeaponShopData _weaponShopSlot;

        private UIManager _uIManager;

        private  void Start()
        {
            InitButton();
        }

        private async  void InitButton()
        {
            changeWeaponType.onClick.AddListener(ChangeWeaponButton);
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
            weaponPrice.text = _weaponShopSlot.WeaponPrice.ToString();
            weaponLevel.text = "LEVEL " + _weaponShopSlot.WeaponLevel.ToString();
            name.text = _weaponShopSlot.Name.ToUpper();
            gameObject.name = _weaponShopSlot.Name;
            image.sprite = _weaponShopSlot.Image;
            
        }


        private void ChangeWeaponButton()
        {
            _uIManager.ChangeWeaponType(weaponType);
        }

        private void BuyWeaponButton()
        {
            bool IsActive = _uIManager.BuyWeapon(weaponType);
            changeWeaponType.gameObject.SetActive(IsActive);//WeaponType will Activate; 
            buyWeaponButton.gameObject.SetActive(IsActive);
            upgradeWeaponButton.gameObject.SetActive(!IsActive);
        }

        private void UpgradeWeaponButton()
        {
            _weaponShopSlot = _uIManager.UpgradeWeapon(weaponType);

            weaponPrice.text = _weaponShopSlot.WeaponPrice.ToString();
            weaponLevel.text = "LEVEL " + _weaponShopSlot.WeaponLevel.ToString();
        }
        public void SetWeaponType(WeaponTypes weaponTypes)
        {
            weaponType = weaponTypes;
            InitWeapon();
        }
    }
}