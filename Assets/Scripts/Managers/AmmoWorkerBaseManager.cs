using AIBrain;
using Controllers;
using Data.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Interfaces;

namespace Managers
{
    public class AmmoWorkerBaseManager : MonoBehaviour,IGetPoolObject
    {
        //#region Self-Private Variabels

        [SerializeField]
        private CD_AIData cD_AIData;

        private int counter;

        private AmmoWorkerAIData _ammoWorkerAIData;
        private GameObject _ammoInstance;

        [SerializeField]//datadan gelcek
        private int _maxAmmoCapasity=15;

        internal void SendAmmoToWorker(Transform WorkerTransform)
        {   
            for (int i = 0; i < _maxAmmoCapasity; i++)
            {
                
                _ammoInstance = GetObject(PoolType.Ammo);
               
            }
        }


        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea += OnAddAmmoWorker;
           // AmmoManagerSignals.Instance.onSetTurretStackList += OnSetTurretStackList;
           // AmmoManagerSignals.Instance.onSetAmmoStackStatus += OnSetAmmoStackStatus;
        }

        private void UnsubscribeEvents()
        {
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea -= OnAddAmmoWorker;
            //AmmoManagerSignals.Instance.onSetTurretStackList -= OnSetTurretStackList;
            //AmmoManagerSignals.Instance.onSetAmmoStackStatus -= OnSetAmmoStackStatus;
        }

        private void OnDisable() => UnsubscribeEvents();


        //internal void WhenExitTurretStack(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.IsLoadTurret(false);

        //  internal void WhenAmmoworkerEnterAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.SetTriggerInfo(true);

        //  internal void WhenAmmoworkerExitAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain) => ammoWorkerBrain.SetTriggerInfo(false);


        public GameObject GetObject(PoolType poolName) => PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        private void OnAddAmmoWorker(Transform ammoWorkerZone)
        {
            AddAmmoWorker(ammoWorkerZone);
        }

        public void AddAmmoWorker(Transform ammoWorkerZone)
        {
            GameObject ammoWorker = GetObject(PoolType.AmmoWorkerAI);

            ammoWorker.transform.position = ammoWorkerZone.position;
        }

       
    }
}



