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
        WeaponTypes weaponType;
        [SerializeField]
        private Image image;
        [SerializeField]
        private TextMeshProUGUI purchasePrice;
        [SerializeField]
        private TextMeshProUGUI key;
        [SerializeField]
        private TextMeshProUGUI weaponPrice;
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


        private bool _isActive=false;
        private  void Start()
        {
            InitButton();

        }
        
        private   void InitButton()
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
       
            weaponPrice.text = _weaponShopSlot.WeaponPrice.ToString();
            weaponLevel.text = "LEVEL " + _weaponShopSlot.WeaponLevel.ToString();
            key.text = _weaponShopSlot.Name.ToUpper();
            gameObject.name = _weaponShopSlot.Name;
            image.sprite = _weaponShopSlot.Image;
            purchasePrice.text=_weaponShopSlot.PurchasePrice.ToString();
            _isActive = _weaponShopSlot.WeaponHasSold;

            Debug.Log(_isActive);

            buyWeaponButton.gameObject.SetActive(!_isActive);
            selectWeaponType.gameObject.SetActive(_isActive);
        }


        private void ChangeWeaponButton()
        {   
            _uIManager.ChangeWeaponType(weaponType);
        }

        private void BuyWeaponButton()
        {
            _isActive = _uIManager.BuyWeapon(weaponType);
            selectWeaponType.gameObject.SetActive(_isActive);
            buyWeaponButton.gameObject.SetActive(_isActive);

        }

        private void UpgradeWeaponButton()
        {

            _uIManager.UpgradeWeapon(weaponType);

            weaponPrice.text = _weaponShopSlot.WeaponPrice.ToString();
            weaponLevel.text = "LEVEL " + _weaponShopSlot.WeaponLevel.ToString();
        }

        public void SetWeaponType(WeaponTypes weaponTypes)
        {
            weaponType = weaponTypes;
        }
    }
}