using Datas.ValueObject;
using Enums;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class WorkerSopUIController : MonoBehaviour
    {
        [SerializeField]
        WorkerUpgradeType workerUpgradeType;
        [SerializeField]
        private Image image;
        [SerializeField]
        private TextMeshProUGUI key;
        [SerializeField]
        private TextMeshProUGUI feturesPrice;
        [SerializeField]
        private TextMeshProUGUI feturesLevel;
        [SerializeField]
        private Button upgradeWeaponButton;

        private WorkerShopData _workerShopSlot;

        private UIManager _uIManager;

        private void Start() => InitButton();

        private  void InitButton() => upgradeWeaponButton.onClick.AddListener(UpgradeWorkerButton);

        internal void SetToShopData(List<WorkerShopData> workerShopSlot, UIManager uIManager)
        {
            _workerShopSlot = workerShopSlot[(int)workerUpgradeType];
            _uIManager = uIManager;
            InitFertures();
        }
        private void InitFertures()
        {   
            feturesPrice.text = _workerShopSlot.UpgradePrice.ToString();
            feturesLevel.text = "LEVEL " + _workerShopSlot.UpgradeLevel.ToString();
            key.text = _workerShopSlot.Name.ToUpper();
            gameObject.name = _workerShopSlot.Name;
            image.sprite = _workerShopSlot.Image;

        }

        private void UpgradeWorkerButton()
        {
            
            _uIManager.UpgradeWorkerButton(workerUpgradeType);
            feturesPrice.text = _workerShopSlot.UpgradePrice.ToString();
            feturesLevel.text = "LEVEL " + _workerShopSlot.UpgradeLevel.ToString();
        }


        internal void SetWeaponType(WorkerUpgradeType workerUpgradeTypes)
        {
            workerUpgradeType = workerUpgradeTypes;
        }
    }
}