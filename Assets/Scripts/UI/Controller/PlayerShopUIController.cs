using Datas.ValueObject;
using Enums;
using Managers;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class PlayerShopUIController : MonoBehaviour
    {
        [SerializeField]
        private PlayerUpgradeType playerUpgradeType;

        [SerializeField]
        private Image image;

        [SerializeField]
        private TextMeshProUGUI Key;

        [SerializeField]
        private TextMeshProUGUI feturesPrice;

        [SerializeField]
        private TextMeshProUGUI feturesLevel;

        [SerializeField]
        private Button upgradeWeaponButton;

        private PlayerShopData _playerShopData;

        private UIManager _uIManager;

        private void Start()
        {
            InitButton();
        }

        private void InitButton()
        {
            upgradeWeaponButton.onClick.AddListener(UpgradePlayerButton);
        }

        internal void SetToShopData(List<PlayerShopData> playerShopSlot, UIManager uIManager)
        {
            _playerShopData = playerShopSlot[(int)playerUpgradeType];
            _uIManager = uIManager;
            InitFertures();
        }

        private void InitFertures()
        {
            feturesPrice.text = _playerShopData.UpgradePrice.ToString();
            feturesLevel.text = "LEVEL " + _playerShopData.UpgradeLevel.ToString();
            Key.text = _playerShopData.Name.ToUpper();
            gameObject.name = _playerShopData.Name;
            image.sprite = _playerShopData.Image;
        }

        private void UpgradePlayerButton()
        {
            _uIManager.UpgradePlayerButton(playerUpgradeType);

            feturesPrice.text = _playerShopData.UpgradePrice.ToString();
            feturesLevel.text = "LEVEL " + _playerShopData.UpgradeLevel.ToString();
        }

        internal void SetWeaponType(PlayerUpgradeType playerUpgradeTypes)
        {
            playerUpgradeType = playerUpgradeTypes;
        }
    }
}