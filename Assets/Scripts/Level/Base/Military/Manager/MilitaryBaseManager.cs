using AIBrains.SoldierBrain;
using Data.ValueObject;
using Enums;
using Interfaces;
using Signals;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Managers
{
    public class MilitaryBaseManager : MonoBehaviour, IGetPoolObject
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField]
        private Transform tentTransfrom;

        [SerializeField]
        private Transform slotTransform;

        [SerializeField]
        private Transform frontYardPosition;

        [SerializeField]
        private GameObject slotZonePrefab;

        #endregion Serialized Variables

        #region Private Variables

        private MilitaryBaseData _data;

        private bool _isBaseAvaliable;
        private bool _isTentAvaliable = true;
        private int _totalAmount;
        [ShowInInspector] private List<Vector3> _slotTransformList = new List<Vector3>();
        private List<SoldierAIBrain> _soldierList = new List<SoldierAIBrain>();

        #endregion Private Variables

        #endregion Self Variables

        private void Awake()
        {
            _data = GetBaseData();
        }

        private MilitaryBaseData GetBaseData()
        {
            return InitializeDataSignals.Instance.onLoadMilitaryBaseData.Invoke();
        }

        public IEnumerator Start()
        {
            if (_data.CurrentSoldierAmount == 0)
                yield break;
            yield return new WaitForSeconds(1f);
            StartCoroutine(soldierEnumerator());
            yield return new WaitForSeconds(3f);
            StopCoroutine(soldierEnumerator());
        }

        private IEnumerator soldierEnumerator()
        {
            OnSoldiersInit(_data.CurrentSoldierAmount);
            yield return null;
        }

        private void OnSoldiersInit(int soldierCount)
        {
            for (var i = 0; i < soldierCount; i++)
            {
                GetSoldier();
            }
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            AISignals.Instance.onSoldierActivation += OnSoldierActivation;
            AISignals.Instance.onSoldierAmountUpgrade += OnSoldierAmountUpgrade;
            AISignals.Instance.onGenerateSoldier += OnGenerateSoldier;
        }

        private void UnsubscribeEvents()
        {
            AISignals.Instance.onSoldierActivation -= OnSoldierActivation;
            AISignals.Instance.onSoldierAmountUpgrade -= OnSoldierAmountUpgrade;
            AISignals.Instance.onGenerateSoldier += OnGenerateSoldier;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion Event Subscription

        private void OnSoldierActivation()
        {
            var soldierCount = _soldierList.Count - 1;
            for (var i = 0; i < soldierCount + 1; i++)
            {
                _soldierList[soldierCount - i].HasSoldiersActivated = true;
                _soldierList.RemoveAt(soldierCount - i);
                _soldierList.TrimExcess();
            }
            _isTentAvaliable = true;
            _data.CurrentSoldierAmount = 0;
        }

        private void GetSoldier()
        {
            var soldierAIPrefab = GetObject(PoolType.SoldierAI);
            var soldierBrain = soldierAIPrefab.GetComponent<SoldierAIBrain>();
            _soldierList.Add(soldierBrain);
            SetSlotZoneTransformsToSoldiers(soldierBrain);
        }

        private void SetSlotZoneTransformsToSoldiers(SoldierAIBrain soldierBrain)
        {
            soldierBrain.GetSlotTransform(_slotTransformList[_data.CurrentSoldierAmount]);
            soldierBrain.TentPosition = tentTransfrom;
            soldierBrain.FrontYardStartPosition = frontYardPosition;
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

        private void OnSoldierAmountUpgrade()
        {
            UpdateSoldierAmount();
        }

        private void OnGenerateSoldier()
        {
            UpdateSoldierAmount();
        }

        private async void UpdateSoldierAmount()
        {
            if (!_isTentAvaliable) return;
            if (_data.CurrentSoldierAmount < _data.TentCapacity)
            {
                GetSoldier();
                _data.CurrentSoldierAmount += 1;
                await Task.Delay(100);
                UpdateSoldierAmount();
            }
            else
            {
                _isTentAvaliable = false;
                _data.CurrentSoldierAmount = 0;
            }
        }

        public void GetStackPositions(List<Vector3> gridPositionData)
        {
            for (int i = 0; i < gridPositionData.Count; i++)
            {
                _slotTransformList.Add(gridPositionData[i]);
                var obj = Instantiate(slotZonePrefab, gridPositionData[i], quaternion.identity, slotTransform);
            }
        }

        #region Pool Signals

        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        }

        #endregion Pool Signals

        #region SaveSignals

        [Button]
        private void SaveData()
        {
            InitializeDataSignals.Instance.onSaveMilitaryBaseData.Invoke(_data);
        }

        private void OnApplicationQuit()
        {
            InitializeDataSignals.Instance.onSaveMilitaryBaseData.Invoke(_data);
        }

        #endregion SaveSignals
    }
}