using Enums;
using Extentions;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        

        public UnityAction<int> onChangeMoney =delegate { }; 

        public UnityAction<int> onChangeDiamon = delegate { };

        public UnityAction<int> onChangeUpgradeAmount =delegate { };

        public UnityAction<ShopType> onGetShopTypeOnEnter = delegate { };

        public UnityAction<ShopType> onGetShopTypeOnExit = delegate { };

        public UnityAction<WeaponTypes> onChangeWeaponType = delegate { };

        public Func<WeaponTypes, int> onPressUpgradeButton = delegate { return 0; };

        public Func<WeaponTypes, bool> onPressUnlockButton = delegate { return false; };

        public Func<WorkerUpgradeType, int> onPressWorkersUpgradeButtons = delegate { return 0; };

        public Func<PlayerUpgradeType, int> onPressPlayerUpgradeButtons = delegate { return 0; };

        public Func<SoldierUpgradeType, int> onPressSoldierUpgradeButton = delegate { return 0; };








    }
}