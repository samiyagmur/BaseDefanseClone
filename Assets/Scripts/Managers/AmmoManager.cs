using AIBrain;
using Controllers;
using Data.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using System;
using UnityEngine;

namespace Managers
{
    

    public class AmmoManager : MonoBehaviour
    {
        #region Self-Private Variabels
        [SerializeField]
        private CD_AIData cD_AIData;

        private int counter;

        private AmmoWorkerAIData _ammoWorkerAIData;

        private GameObject _targetStack;

      // private PlayerAmmoStackStatus _playerAmmoStackStatus;

        private AmmoWorkerBrain _ammoWorkerBrain;
        #endregion
        internal void Awake() => Init();

        private void Init() => _ammoWorkerAIData = cD_AIData.AmmoWorkerAIDatas;

 

        #region Event Subscription
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea += OnPlayerEnterAmmoWorkerCreaterArea;
            AmmoManagerSignals.Instance.onAmmoStackStatus += OnAmmoStackStatus;

        }
        private void UnsubscribeEvents()
        {
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea -= OnPlayerEnterAmmoWorkerCreaterArea;
            AmmoManagerSignals.Instance.onAmmoStackStatus += OnAmmoStackStatus;
        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion

        public void IsExitOnTurretStack(AmmoWorkerStackController ammoWorkerStackController) => ammoWorkerStackController.SetClearWorkerStackList();

        internal void IsAmmoWorkerStackEmpty(AmmoWorkerBrain ammoWorkerBrain) => _ammoWorkerBrain= ammoWorkerBrain;

        internal void IsEnterTurretStack(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.IsLoadTurret(true);

        internal void IsExitTurretStack(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.IsLoadTurret(false);

        internal void IsAmmoEnterAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.SetTriggerInfo(true);

        internal void IsAmmoExitAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.SetTriggerInfo(false);

        internal void SetTargetCurrentTurretStack(AmmoWorkerBrain ammoWorkerBrain) =>
            ammoWorkerBrain.SetTargetTurretStack(AmmoManagerSignals.Instance.onSetConteynerList.Invoke());


        internal void IsStayOnAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain,AmmoWorkerStackController ammoWorkerStackController)
        {           
            
            if (counter < _ammoWorkerAIData.MaxStackCount)
            {
                ammoWorkerStackController.AddStack(_ammoWorkerAIData.AmmoWareHouse, ammoWorkerBrain.gameObject.transform, GetObject(PoolType.Ammo.ToString()));
                counter++;
            }

            else
            {
                ammoWorkerBrain.IsStackFul(AmmoStackStatus.Full);
            }

        }

        private void OnPlayerEnterAmmoWorkerCreaterArea(Transform workerCreater) => AddAmmaWorker(workerCreater);


        private void OnAmmoStackStatus(AmmoStackStatus status) => _ammoWorkerBrain.IsStackFul(status);


        public GameObject GetObject(string poolName) => ObjectPoolManager.Instance.GetObject<GameObject>(poolName);

        public void AddAmmaWorker(Transform workerCreater)
        {
            GameObject ammoWorker = GetObject(PoolType.AmmoWorkerAI.ToString());

            ammoWorker.transform.position = workerCreater.position;
        }
      
        public void ResetItems() => counter = 0;




        #region Subscirabe Event methods

        #endregion

        #region Physics Methods

        #endregion

        #region SendInfo

        #endregion


    }
}