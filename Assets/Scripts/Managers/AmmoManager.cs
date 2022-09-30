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


        AmmoWorkerStackController _ammoWorkerStackController;

        [SerializeField]
        private List<GameObject> _ammoWorkerList=new List<GameObject>();
       
      
        private int counter;

        private int WhenCallFirstTime=0;
        private AmmoWorkerBrain ammoWorkerBrain;

        private AmmoWorkerAIData _ammoWorkerAIData;
        private GameObject _targetContayner;
        private List<GameObject> _emtyAmmoWorkerStack;
        #endregion
        internal void Awake() => Init();

        private void Init()
        {
            _ammoWorkerAIData = cD_AIData.AmmoWorkerAIDatas;
           
        }

      
        #region Event Subscription
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            AmmoManagerSignals.Instance.onSetConteynerList += OnSetConteynerList;
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea += OnPlayerEnterAmmoWorkerCreaterArea;
        }

        private void UnsubscribeEvents()
        {
            AmmoManagerSignals.Instance.onSetConteynerList -= OnSetConteynerList;
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea -= OnPlayerEnterAmmoWorkerCreaterArea;
        }

        private void OnDisable() => UnsubscribeEvents();

        
        #endregion


        internal void IsEnterTurretContayner() => ammoWorkerBrain.IsLoadTurret(true);

        internal void IsExitTurretContayner() => ammoWorkerBrain.IsLoadTurret(false);

        internal void IsAmmoEnterAmmoWareHouse() => ammoWorkerBrain.SetTriggerInfo(true);

        internal void IsAmmoExitAmmoWareHouse() => ammoWorkerBrain.SetTriggerInfo(false);

        internal void IsAmmoWorkerStayOnAmmoWareHouse()
        {
            
            if (counter < _ammoWorkerAIData.MaxStackCount)
            {
            
                ammoWorkerBrain.IsStackFul(PlayerAmmaStackStatus.Empty);

                _ammoWorkerStackController.AddStack(_ammoWorkerAIData.AmmoWareHouse, ammoWorkerBrain.gameObject.transform, GetObject(PoolType.Ammo.ToString()));

                counter++;
            }
            else
            {
                ammoWorkerBrain.IsStackFul(PlayerAmmaStackStatus.Full);
            }

        }
    

        private void OnSetConteynerList(GameObject targetContayner, List<GameObject> emtyAmmoWorkerStack)
        {
           

            _targetContayner = targetContayner;
            _emtyAmmoWorkerStack = emtyAmmoWorkerStack;
            SendTurretContayner();

            
        }

        private void SendTurretContayner()
        {
            
            if (WhenCallFirstTime == 0)
            {
                WhenCallFirstTime++;
                return;
            }

          
            if (_emtyAmmoWorkerStack.Count == 0 || _emtyAmmoWorkerStack == null)
            {
                ammoWorkerBrain.IsStackFul(PlayerAmmaStackStatus.Empty);
                
                _ammoWorkerStackController.SetEmtyWorkerStackList(_emtyAmmoWorkerStack);
            }
                    

            if (_ammoWorkerList.Count != 0)//????delete
                    ammoWorkerBrain.SetTargetTurretContayner(_targetContayner);

           
        }

        private void OnPlayerEnterAmmoWorkerCreaterArea(Transform workerCreater)
        {
            Debug.Log("OnPlayerEnterAmmoWorkerCreaterArea");
            AddAmmaWorker(workerCreater);
            ammoWorkerBrain = _ammoWorkerList[_ammoWorkerList.Count - 1].GetComponent<AmmoWorkerBrain>();
            _ammoWorkerStackController = ammoWorkerBrain.GetComponent<AmmoWorkerStackController>();


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