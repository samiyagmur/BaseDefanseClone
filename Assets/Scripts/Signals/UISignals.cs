using Data.ValueObject;
using Datas.ValueObject;
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

        public UnityAction<int> onChangeDiamond = delegate { };

        public UnityAction<int> onChangeUpgradeAmount =delegate { };

        public UnityAction<ShopType> onGetShopTypeOnEnter = delegate { };

        public UnityAction<ShopType> onGetShopTypeOnExit = delegate { };

        public UnityAction<WeaponTypes> onChangeWeaponType = delegate { };

        public Func<WeaponTypes, WeaponShopData> onPressUpgradeButton = delegate { return null; };

        public Func<WeaponTypes, bool> onPressUnlockButton = delegate { return false; };

        public Func<WorkerUpgradeType, WorkerShopData> onPressWorkersUpgradeButtons = delegate { return null; };

        public Func<PlayerUpgradeType, PlayerShopData> onPressPlayerUpgradeButtons = delegate { return null; };

        public Func<SoldierUpgradeType, SoldierShopData> onPressSoldierUpgradeButton = delegate { return null; };








    }
}