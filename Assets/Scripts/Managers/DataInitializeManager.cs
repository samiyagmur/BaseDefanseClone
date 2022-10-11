using Data.UnityObject;
using Data.ValueObject.LevelData;
using Datas.UnityObject;
using Datas.ValueObject;
using Signals;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class DataInitializeManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField]
        private List<LevelData> levelDatas = new List<LevelData>();


        private CD_Level _cdLevel;

        [SerializeField]
        private CD_Enemy cdEnemy;

        #endregion

        #region Private Variables

        private int _levelID;
        private int _uniqueID = 12123;

        private BaseRoomData _baseRoomData;
        private MineBaseData _mineBaseData;
        private MilitaryBaseData _militaryBaseData;
        private BuyablesData _buyablesData;
        private ShopData _shopData;
        private ScoreData _scoreData;

        #endregion

        #endregion

        private CD_Level GetLevelDatas() => Resources.Load<CD_Level>("Data/CD_Level");
        private void Awake()
        {
             _cdLevel = GetLevelDatas();
            _levelID = _cdLevel.LevelId;
             levelDatas = _cdLevel.LevelDatas;
            _shopData = _cdLevel.ShopData;
            _scoreData= _cdLevel.ScoreData;
        }
        private void Start()
        {
           
            InitData();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
        }

        #region InıtData

        private void InitData()
        {

            if (!ES3.FileExists($"_levelData{_uniqueID}.es3"))
            {
                if (!ES3.KeyExists("_levelData"))
                {
                    _cdLevel = GetLevelDatas();
                    _levelID = _cdLevel.LevelId;
                    levelDatas = _cdLevel.LevelDatas;
                    _shopData = _cdLevel.ShopData;
                    _scoreData = _cdLevel.ScoreData;

                    Save(_uniqueID);
                }
            }

            Load(_uniqueID);
           
        }
        public void Load(int uniqueId)
        {

            CD_Level cdLevel = SaveLoadSignals.Instance.onLoadGameData.Invoke("_levelData", uniqueId);

            _levelID = cdLevel.LevelId;
            levelDatas = cdLevel.LevelDatas;
            _baseRoomData = cdLevel.LevelDatas[_levelID].BaseData.BaseRoomData;
            _mineBaseData = cdLevel.LevelDatas[_levelID].BaseData.MineBaseData;
            _militaryBaseData = cdLevel.LevelDatas[_levelID].BaseData.MilitaryBaseData;
            _buyablesData = cdLevel.LevelDatas[_levelID].BaseData.BuyablesData;
            _shopData = cdLevel.ShopData;
            _scoreData = cdLevel.ScoreData;

           // Debug.Log(_shopData._weaponShopSlot[0].WeaponLevel);
        }


        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnSyncLevel;
            InitializeDataSignals.Instance.onSaveLevelID += OnSyncLevelID;
            InitializeDataSignals.Instance.onSaveBaseRoomData += SyncBaseRoomDatas;
            InitializeDataSignals.Instance.onSaveMineBaseData += SyncMineBaseDatas;
            InitializeDataSignals.Instance.onSaveMilitaryBaseData += SyncMilitaryBaseData;
            InitializeDataSignals.Instance.onSaveBuyablesData += SyncBuyablesData;
            InitializeDataSignals.Instance.onSaveShopData += SyncShopData;
            InitializeDataSignals.Instance.onSaveScoreData += SyncScoreData;

            InitializeDataSignals.Instance.onLoadScoreData += OnLoadScoreData;
            InitializeDataSignals.Instance.onLoadShopData += OnLoadShopData;

            // InitializeDataSignals.Instance.onSaveWeaponData += SyncWeaponData;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnSyncLevel;
            InitializeDataSignals.Instance.onSaveLevelID -= OnSyncLevelID;
            InitializeDataSignals.Instance.onSaveBaseRoomData -= SyncBaseRoomDatas;
            InitializeDataSignals.Instance.onSaveMineBaseData -= SyncMineBaseDatas;
            InitializeDataSignals.Instance.onSaveMilitaryBaseData -= SyncMilitaryBaseData;
            InitializeDataSignals.Instance.onSaveBuyablesData -= SyncBuyablesData;
            InitializeDataSignals.Instance.onSaveShopData -= SyncShopData;
            InitializeDataSignals.Instance.onSaveScoreData -= SyncScoreData;

            InitializeDataSignals.Instance.onLoadScoreData -= OnLoadScoreData;
            InitializeDataSignals.Instance.onLoadShopData -= OnLoadShopData;
            //InitializeDataSignals.Instance.onSaveWeaponData -= SyncWeaponData;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void SendDataToManagers()
        {

            InitializeDataSignals.Instance.onLoadLevelID?.Invoke(_levelID);
            InitializeDataSignals.Instance.onLoadBaseRoomData?.Invoke(_baseRoomData);//
            InitializeDataSignals.Instance.onLoadMineBaseData?.Invoke(_mineBaseData);//
            InitializeDataSignals.Instance.onLoadMilitaryBaseData?.Invoke(_militaryBaseData);//
            InitializeDataSignals.Instance.onLoadBuyablesData?.Invoke(_buyablesData);//
            //InitializeDataSignals.Instance.onLoadAmmoWorkerData?.Invoke(_)
        }
        #region Level Save - Load 
        private void OnApplicationQuit()
        {
            Save(_uniqueID);
        }
        public void Save(int uniqueId)
        {
            CD_Level cdLevel = new CD_Level(_levelID, levelDatas, _shopData, _scoreData);

            SaveLoadSignals.Instance.onSaveGameData.Invoke(cdLevel, uniqueId);
        }

        #endregion


        #region Data Sync
        private void OnSyncLevel()
        {
            SendDataToManagers();
        }

        private void OnSyncLevelID(int levelID)
        {
            _levelID = levelID;
        }
        private void SyncBaseRoomDatas(BaseRoomData baseRoomData)
        {
            _baseRoomData = baseRoomData;
        }

        private void SyncMineBaseDatas(MineBaseData mineBaseData)
        {
            _mineBaseData = mineBaseData;
        }

        private void SyncMilitaryBaseData(MilitaryBaseData militaryBaseData)
        {
            _militaryBaseData = militaryBaseData;
        }

        private void SyncBuyablesData(BuyablesData buyablesData)
        {
            _buyablesData = buyablesData;
        }

        private void SyncShopData(ShopData shopData)
        {
            _shopData = shopData;
        }
        private void SyncScoreData(ScoreData scoredata)
        {   
            _scoreData = scoredata;
        }

        private ScoreData OnLoadScoreData()
        {
            return _scoreData;
        }

        private ShopData OnLoadShopData()
        {  
            return _shopData;
        }


        #endregion

    }
}