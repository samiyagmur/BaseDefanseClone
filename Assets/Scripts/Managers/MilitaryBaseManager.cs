using System;
using System.Collections.Generic;
using AIBrains.SoldierBrain;
using Data.UnityObject;
using Data.ValueObject.LevelData;
using Enums;
using Interfaces;
using Signals;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using ValueObject.AIData;

namespace Managers
{
    public class MilitaryBaseManager : MonoBehaviour,IGetPoolObject,IReleasePoolObject
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private Transform tentTransfrom;
        [SerializeField] private Transform slotTransform;

        #endregion

        #region Private Variables

        private MilitaryBaseData _data;
        private SoldierAIData _soldierAIData;
        private bool _isBaseAvaliable;
        private bool _isTentAvaliable = true;
        private int _totalAmount;
        private int _soldierAmount;
        private List<GameObject> _soldierList = new List<GameObject>();
        [ShowInInspector] private List<Vector3> _slotTransformList = new List<Vector3>();
        private int _tentCapacity;
        #endregion

        #endregion

        private void Awake()
        {
            _soldierAIData = GetSoldierAIData();
        }

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            AISignals.Instance.onSoldierActivation += OnSoldierActivation;
            InitializeDataSignals.Instance.onLoadMilitaryBaseData += OnLoadMilitaryBaseData;
        }
        private void UnsubscribeEvents()
        {
            AISignals.Instance.onSoldierActivation -= OnSoldierActivation;
            InitializeDataSignals.Instance.onLoadMilitaryBaseData -= OnLoadMilitaryBaseData;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void OnLoadMilitaryBaseData(MilitaryBaseData data)
        {
            _data = data;
        }

        private MilitaryBaseData GetBaseData() =>
            Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[0].BaseData.MilitaryBaseData;
        private SoldierAIData GetSoldierAIData() => Resources.Load<CD_AIData>("Data/CD_AIData").SoldierAIDatas;
        private void OnSoldierActivation()
        {
            _isTentAvaliable = true;
        }
        public GameObject GetObject(PoolType poolName)
        {
            var soldierAIPrefab = PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
            var soldierBrain = soldierAIPrefab.GetComponent<SoldierAIBrain>();
            SetSlotZoneTransformsToSoldiers(soldierBrain);
            return soldierAIPrefab;
        }
        private void SetSlotZoneTransformsToSoldiers(SoldierAIBrain soldierBrain)
        {
            soldierBrain.GetSlotTransform(_slotTransformList[_soldierAmount]);
            soldierBrain.TentPosition = tentTransfrom;
            soldierBrain.FrontYardStartPosition = _data.frontYardSoldierPosition;
        }
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName, obj);
        }

        public void UpdateTotalAmount(int Amount)
        {
            if (!_isBaseAvaliable) return;
            if (_totalAmount < _data.BaseCapacity)
            {
                _totalAmount += Amount;
            }
            else
            {
                _isBaseAvaliable = false;
            }
        }

        [Button]
        public void UpdateSoldierAmount()
        {
            if (!_isTentAvaliable) return;
            if (_soldierAmount < _data.TentCapacity)
            {
                GetObject(PoolType.SoldierAI);
                _soldierAmount += 1;
            }
            else
            {
                _isTentAvaliable = false;
                _soldierAmount = 0;
            }
        }
        public void GetStackPositions(List<Vector3> gridPositionData)
        {
            for (int i = 0; i < gridPositionData.Count; i++)
            {
                _slotTransformList.Add(gridPositionData[i]);
                var obj = Instantiate(_data.SlotPrefab, gridPositionData[i], quaternion.identity, slotTransform);
            }
        }

       
    }
}

