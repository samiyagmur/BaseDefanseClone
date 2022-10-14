using Data.ValueObject;
using Data.ValueObject.LevelData;
using Datas.ValueObject;
using Extentions;
using System;
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
        public UnityAction<ShopData> onSaveShopData = delegate (ShopData arg0) { };
        public UnityAction<ScoreData> onSaveScoreData = delegate (ScoreData arg0) { };

        // public UnityAction<WeaponListData> onSaveWeaponData=delegate (WeaponListData arg0) { };

        public Func<MilitaryBaseData> onLoadMilitaryBaseData = delegate { return null; };
        public Func<BaseRoomData> onLoadBaseRoomData = delegate { return null; };
        public Func<BuyablesData> onLoadBuyablesData = delegate { return null; };
        public Func<MineBaseData> onLoadMineBaseData = delegate { return null; };
        public UnityAction<int> onLoadLevelID = delegate (int arg0) { };
        public Func<ShopData> onLoadShopData = delegate { return null; };
        public Func<ScoreData> onLoadScoreData = delegate { return null; };

        //  public UnityAction<WeaponListData> onLoadWeaponData = delegate (WeaponListData arg0) { };
    }
}