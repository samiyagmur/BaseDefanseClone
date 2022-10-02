using Abstraction;
using AIBrain;
using Controllers;
using Data.UnityObject;
using Datas.ValueObject;
using Enums;
using Interfaces;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    

    public class AmmoManager : MonoBehaviour
    {
        #region Self-Private Variabels
        [SerializeField]
        private CD_AIData cD_AIData;


        [SerializeField]
        private List<GameObject> _ammoWorkerList=new List<GameObject>();
       
      
        private int counter;

        internal void SendThisEnterAmmoManager(GameObject gameObject)
        {
            throw new NotImplementedException();
        }

        private int WhenCallFirstTime=0;
        private AmmoWorkerBrain ammoWorkerBrain;

        private AmmoWorkerAIData _ammoWorkerAIData;

        internal void SendThisEnterTurretStack(GameObject gameObject)
        {
            throw new NotImplementedException();
        }

        private GameObject _targetStack;
        private List<GameObject> _emtyAmmoWorkerStack;
        #endregion
        internal void Awake() => Init();

        private void Init()
        {
            _ammoWorkerAIData = cD_AIData.AmmoWorkerAIDatas;
           
        }

      
        #region Event Subscription
        private void OnEnable() => SubscribeEvents();

        internal void SendThisExitAmmoManager(GameObject gameObject)
        {
            throw new NotImplementedException();
        }

        private void SubscribeEvents()
        {
            AmmoManagerSignals.Instance.onSetConteynerList += OnSetConteynerList;   
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea += OnPlayerEnterAmmoWorkerCreaterArea;
        }

        internal void SendThisExitTurretStack(GameObject gameObject)
        {
            throw new NotImplementedException();
        }

        private void UnsubscribeEvents()
        {
            AmmoManagerSignals.Instance.onSetConteynerList -= OnSetConteynerList;
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea -= OnPlayerEnterAmmoWorkerCreaterArea;
        }

        private void OnDisable() => UnsubscribeEvents();


        #endregion

        public void IsExitOnTurretContayner(AmmoWorkerStackController ammoWorkerStackController) => ammoWorkerStackController.SetClearWorkerStackList();
        internal void IsEnterTurretContayner(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.IsLoadTurret(true);

        internal void IsExitTurretContayner(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.IsLoadTurret(false);

        internal void IsAmmoEnterAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.SetTriggerInfo(true);

        internal void IsAmmoExitAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.SetTriggerInfo(false);

        internal void IsStayOnAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain,AmmoWorkerStackController ammoWorkerStackController)
        {           
            
            if (counter < _ammoWorkerAIData.MaxStackCount)
            {
            
                ammoWorkerBrain.IsStackFul(PlayerAmmaStackStatus.Empty);

                ammoWorkerStackController.AddStack(_ammoWorkerAIData.AmmoWareHouse, ammoWorkerBrain.gameObject.transform, GetObject(PoolType.Ammo.ToString()));

                counter++;
            }
            else
            {
                ammoWorkerBrain.IsStackFul(PlayerAmmaStackStatus.Full);
            }

        }

        

        private void OnSetConteynerList(GameObject targetStack)
        {

            _targetStack = targetStack;
            SendTurretContayner();

            
        }

        private void SendTurretContayner()//delete??
        {
            
            if (WhenCallFirstTime == 0)
            {
                Debug.Log("WhenCallFirstTime");
                WhenCallFirstTime++;
                return;
            }
                ammoWorkerBrain.IsStackFul(PlayerAmmaStackStatus.Empty);

                    
                ammoWorkerBrain.SetTargetTurretContayner(_targetStack);

           
        }

        private void OnPlayerEnterAmmoWorkerCreaterArea(Transform workerCreater)
        {

            AddAmmaWorker(workerCreater);

            ammoWorkerBrain = _ammoWorkerList[_ammoWorkerList.Count - 1].GetComponent<AmmoWorkerBrain>();

            SendTurretContayner();

        }

        public void AddAmmaWorker(Transform workerCreater)
        {
            GameObject ammoWorker = GetObject(PoolType.AmmoWorkerAI.ToString());
            ammoWorker.transform.position = workerCreater.position;
            _ammoWorkerList.Add(ammoWorker);
        }
        public GameObject GetObject(string poolName)
        {
            return ObjectPoolManager.Instance.GetObject<GameObject>(poolName);
        }

        public void ResetItems()
        {
            counter = 0;
        }

       


        #region Subscirabe Event methods

        #endregion

        #region Physics Methods

        #endregion

        #region SendInfo

        #endregion


    }
}