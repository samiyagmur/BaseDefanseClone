﻿using Data.ValueObject;
using Data.ValueObject.LevelData;
using Datas.ValueObject;
using Extentions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class InitializeDataSignals : MonoSingleton<InitializeDataSignals>
    {
        public UnityAction<BaseRoomData> onSaveBaseRoomData = delegate (BaseRoomData arg0) { };
        public UnityAction<MineBaseData> onSaveMineBaseData = delegate (MineBaseData arg0) { };
        public UnityAction<MilitaryBaseData> onSaveMilitaryBaseData = delegate (MilitaryBaseData arg0) { };
        public UnityAction<BuyablesData> onSaveBuyablesData = delegate (BuyablesData arg0) { };
        public UnityAction<int> onSaveLevelID = delegate (int arg0) { };
       // public UnityAction<WeaponListData> onSaveWeaponData=delegate (WeaponListData arg0) { };


        public UnityAction<BaseRoomData> onLoadBaseRoomData = delegate (BaseRoomData arg0) { };
        public UnityAction<MineBaseData> onLoadMineBaseData = delegate (MineBaseData arg0) { };
        public UnityAction<MilitaryBaseData> onLoadMilitaryBaseData = delegate (MilitaryBaseData arg0) { };
        public UnityAction<BuyablesData> onLoadBuyablesData = delegate (BuyablesData arg0) { };
        public UnityAction<int> onLoadLevelID = delegate (int arg0) { };
      //  public UnityAction<WeaponListData> onLoadWeaponData = delegate (WeaponListData arg0) { };
    }
}