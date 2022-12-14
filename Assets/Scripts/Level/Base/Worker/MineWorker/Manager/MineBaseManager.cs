using AI.MinerAI;
using Data.UnityObject;
using Data.ValueObject;
using Enum;
using Enums;
using Interfaces;
using Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class MineBaseManager : MonoBehaviour, IGetPoolObject
    {
        #region Self Variables



        #region Serialized Variables

        [SerializeField]
        private TextMeshPro gemWorkerText;

        [SerializeField]
        private List<Transform> mineLocations;//Cd_obkjeyegidecek.

        [SerializeField]
        private List<Transform> cartLocations;

        [SerializeField]
        private Transform gemHolderPosition;

        [SerializeField]
        private Transform instantiatePosition;

        #endregion Serialized Variables

        #region Private Variables

        private int _currentLevel; //LevelManager uzerinden cekilecek
        private int _currentWorkerAmount;
        private int _maxWorkerAmount;
        private Dictionary<MinerAIBrain, GameObject> _mineWorkers = new Dictionary<MinerAIBrain, GameObject>();
        private MineBaseData _mineBaseData;

        #endregion Private Variables

        #endregion Self Variables

        private void Awake()
        {
            _mineBaseData = GetMineBaseData();
            AssignDataValues();
        }

        private void Start()
        {
            InstantiateAllMiners();
            AssignMinerValuesToDictionary();
        }

        private void AssignDataValues()
        {
            _currentWorkerAmount = _mineBaseData.CurrentWorkerAmount;
            _maxWorkerAmount = _mineBaseData.MaxWorkerAmount;
        }

        #region Event Subscription

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            MineBaseSignals.Instance.onGetRandomMineTarget += GetRandomMineTarget;
            MineBaseSignals.Instance.onGetGemHolderPos += OnGetGemHolderPos;
            MineBaseSignals.Instance.onNewMineWorkerAdd += OnNewMineWorkerAdd;
        }

        private void UnSubscribeEvents()
        {
            MineBaseSignals.Instance.onGetRandomMineTarget -= GetRandomMineTarget;
            MineBaseSignals.Instance.onGetGemHolderPos -= OnGetGemHolderPos;
            MineBaseSignals.Instance.onNewMineWorkerAdd -= OnNewMineWorkerAdd;
        }

        private void OnDisable() => UnSubscribeEvents();

        #endregion Event Subscription

        private void OnNewMineWorkerAdd(MinerAIBrain minerBrainAi)
        {
            _currentWorkerAmount++;
            _mineWorkers.Add(minerBrainAi, minerBrainAi.gameObject);

            //InitializeDataSignals.Instance.onSaveMineBaseData()
        }

        private Transform OnGetGemHolderPos()
        {
            return gemHolderPosition;
        }

        private void InstantiateAllMiners()
        {
            for (int index = 0; index < _currentWorkerAmount; index++)
            {
                GameObject _currentObject = GetObject(PoolType.MinerWorkerAI);
                MinerAIBrain _currentMinerAIBrain = _currentObject.GetComponent<MinerAIBrain>();
                _currentObject.transform.position = instantiatePosition.position;
                _mineWorkers.Add(_currentMinerAIBrain, _currentObject);
            }
        }

        private void AssignMinerValuesToDictionary()
        {
            for (int index = 0; index < _mineWorkers.Count; index++)
            {
                _mineWorkers.ElementAt(index).Key.GemHolder = gemHolderPosition;
            }
        }

        public Tuple<Transform, GemMineType> GetRandomMineTarget()
        {
            int randomMineTargetIndex = Random.Range(0, mineLocations.Count + cartLocations.Count);
            return randomMineTargetIndex >= mineLocations.Count
                ? Tuple.Create(cartLocations[randomMineTargetIndex % cartLocations.Count], GemMineType.Cart)
                : Tuple.Create(mineLocations[randomMineTargetIndex], GemMineType.Mine);//Tuple ile enum donecek maden tipine gore animasyon degisecek stateler uzerinden
        }

        public MineBaseData GetMineBaseData() => Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[_currentLevel].BaseData.MineBaseData;

        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        }
    }
}