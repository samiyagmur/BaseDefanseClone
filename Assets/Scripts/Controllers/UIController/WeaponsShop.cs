using Enums;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    [Serializable]
    public class WeaponsShop 
    {   

        public WeaponTypes weaponType;
        public TextMeshProUGUI Price;
        public TextMeshProUGUI Level;
        public Button Unlock;
        public Button Upgrade;
        public Button Select;
    }
}