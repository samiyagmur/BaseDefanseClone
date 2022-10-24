using System.Collections.Generic;
using UnityEngine;
using Signals;
using System.Linq;
using Data.UnityObject;
using Enums;
using Sirenix.OdinInspector;
using Interfaces;
using AIBrain.MoneyWorkers;
using Data.ValueObject;
using Abstraction;
using Concreate;
using System;

namespace Managers
{
    public class MoneyWorkerManager : MonoBehaviour ,IGetPoolObject, IReleasePoolObject
    {
        #region Self variables 

        #region Private Variables

        [ShowInInspector]
        private List<StackableMoney> _targetList = new List<StackableMoney>();
        [ShowInInspector]
        private List<MoneyWorkerAIBrain> _workerList = new List<MoneyWorkerAIBrain>();
        [ShowInInspector]
        private List<Vector3> _slotTransformList = new List<Vector3>();

        private int _currentWorker=0;

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            MoneyWorkerSignals.Instance.onGetMoneyAIData += OnGetWorkerAIData;
            MoneyWorkerSignals.Instance.onGetTransformMoney += OnSendMoneyPositionToWorkers;
            MoneyWorkerSignals.Instance.onThisMoneyTaken += OnThisMoneyTaken;
            MoneyWorkerSignals.Instance.onSetStackable += OnAddMoneyPositionToList;
            MoneyWorkerSignals.Instance.onRemoveStackable += OnRemoveStackableFromTargetList;
            MoneyWorkerSignals.Instance.onGenerateMoneyWorker += CreateMoneyWorker;
        }


        private void UnsubscribeEvents()
        {
            MoneyWorkerSignals.Instance.onGenerateMoneyWorker -= CreateMoneyWorker;
            MoneyWorkerSignals.Instance.onGetMoneyAIData -= OnGetWorkerAIData;
            MoneyWorkerSignals.Instance.onThisMoneyTaken -= OnThisMoneyTaken;
            MoneyWorkerSignals.Instance.onSetStackable -= OnAddMoneyPositionToList;
            MoneyWorkerSignals.Instance.onRemoveStackable -= OnRemoveStackableFromTargetList;
            MoneyWorkerSignals.Instance.onGetTransformMoney -= OnSendMoneyPositionToWorkers;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private WorkerAITypeData OnGetWorkerAIData(WorkerType type)
        {
            return Resources.Load<CD_WorkerAI>("Data/CD_WorkerAI").WorkerAIData.WorkerAITypes[(int)type];
        }

        private void OnAddMoneyPositionToList(StackableMoney pos)
        {   
            _targetList.Add(pos);
        }
        private void OnRemoveStackableFromTargetList(StackableMoney pos)
        {
            if (_targetList.Count <= 0) return;

            _targetList.Remove(pos);
        }

        private Transform OnSendMoneyPositionToWorkers(Transform workerTransform)
        {
          
            if (_targetList.Count == 0)
            {
                Debug.Log("TargetList is null");
                return null;
            }
                

            var _targetT = _targetList.OrderBy(t => (t.transform.position - workerTransform.transform.position).sqrMagnitude)
            .Where(t => !t.IsSelected)
            .Take(1)
            .OrderBy(t => UnityEngine.Random.Range(0, int.MaxValue))
            .LastOrDefault();
            _targetT.IsSelected = true;
            Debug.Log(_targetT.name);
            return _targetT.transform;
           

        }

        private void SendMoneyPositionToWorkers(Transform workerTransform)
        {
            OnSendMoneyPositionToWorkers(workerTransform);
        }
        private void OnThisMoneyTaken()
        {
            var removedObj = _targetList.Where(t => t.IsCollected).FirstOrDefault();
            _targetList.Remove(removedObj);
            _targetList.TrimExcess();

            foreach (var t in _workerList.Where(t => t.CurrentTarget == removedObj))
            {
                SendMoneyPositionToWorkers(t.transform);
            }
        }
        public void GetStackPositions(List<Vector3> gridPos)
        {
            for (int i = 0; i < gridPos.Count; i++)
            {
                _slotTransformList.Add(gridPos[i]);
            }
        }
        private void SetWorkerPosition(MoneyWorkerAIBrain workerAIBrain)
        {
            workerAIBrain.SetInitPosition(_slotTransformList[0]);
            _slotTransformList.RemoveAt(0);
            _slotTransformList.TrimExcess();
        }
        private void CreateMoneyWorker(Transform genarateArea)
        {
            if (OnGetWorkerAIData(WorkerType.MoneyWorkerAI).MaxWorkerAmount == _currentWorker)
                return;
            var obj = GetObject(PoolType.MoneyWorkerAI);
            obj.transform.position = genarateArea.position;
            var objComp = obj.GetComponent<MoneyWorkerAIBrain>();
            _workerList.Add(objComp);
            _currentWorker++;
            SetWorkerPosition(objComp);
        }

        [Button("Release Worker")]
        private void ReleaseMoneyWorker()
        {
            if (_workerList[0])
            {
                var obj = _workerList[0];
                ReleaseObject(obj.gameObject, PoolType.MoneyWorkerAI);
                _workerList.Remove(obj);
            }
        }

        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName, obj);
        }

        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        }
        #endregion
    }
}
