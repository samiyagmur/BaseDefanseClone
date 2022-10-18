using AIBrain;
using AIBrain.AmmoWorkers;
using Controllers;
using Data.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using System;
using System.Collections.Generic;
using System.Linq;
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

        TurretStackGridController turretStackGridController;

        [SerializeField]
        List<TurretStackController> turretStackController=new List<TurretStackController>();
        [SerializeField]
        Queue<AmmoWorkerBrain> _currentAmmoWorkersInTurretStackArae=new Queue<AmmoWorkerBrain>();
        [SerializeField]
        private List<TurretStackController> selectTargetList=new List<TurretStackController>();
        [SerializeField]
        private List<bool> IsAmmoWorkerWentToTarget = new List<bool>();

        private TurretStackController _selectedTarget;
        [SerializeField]
        private List<TurretStackController> _turretStackControllerHolder = new List<TurretStackController>(); 
        #endregion
        internal void Awake() => Init();

        private void Init()
        {
            _ammoWorkerAIData = cD_AIData.AmmoWorkerAIDatas;
            turretStackGridController=new TurretStackGridController();
        }
        private void Start()
        {
            turretStackGridController.GanarateGrid();
        }
        #region Event Subscription
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea += OnPlayerEnterAmmoWorkerCreaterArea;
            AmmoManagerSignals.Instance.onSetTurretStackList += OnSetTurretStackList;
            AmmoManagerSignals.Instance.onSetAmmoStackStatus += OnSetAmmoStackStatus;
          //  AmmoManagerSignals.Instance.onGetAmmoToFire -= OnGetAmmoToFire;

        }

        private void UnsubscribeEvents()
        {
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea -= OnPlayerEnterAmmoWorkerCreaterArea;
            AmmoManagerSignals.Instance.onSetTurretStackList -= OnSetTurretStackList;
            AmmoManagerSignals.Instance.onSetAmmoStackStatus -= OnSetAmmoStackStatus;
           // AmmoManagerSignals.Instance.onGetAmmoToFire -= OnGetAmmoToFire;
        }

        //private GameObject OnGetAmmoToFire(TurretKey arg)
        //{
            
        //}

        private void OnDisable() => UnsubscribeEvents();

        #endregion

        private void OnSetAmmoStackStatus(AmmoStackStatus status)
        {
            Debug.Log(status);

            if (_currentAmmoWorkersInTurretStackArae.Count != 0)
            {
                _currentAmmoWorkersInTurretStackArae.Peek().ChangeAmmoWorkerStackStatus(status);

                _currentAmmoWorkersInTurretStackArae.Dequeue();
            }
        }

        private void OnSetTurretStackList(TurretStackController stackController)
        {
            _turretStackControllerHolder.Add(stackController);
        }
        internal void GiveTargetInAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain)
        {   
            turretStackController = _turretStackControllerHolder;
            

            if (selectTargetList.Count == 0)
            {   
                selectTargetList = turretStackController.OrderBy(x => x.GetCurrentCount()).ToList();
            }

            _selectedTarget = selectTargetList[0];

            selectTargetList.RemoveAt(0);

            selectTargetList.TrimExcess();

            ammoWorkerBrain.SetTargetTurretStack(_selectedTarget.gameObject);
        }


        internal void WhenEnterTurretStack(AmmoWorkerBrain ammoWorkerBrain)
        {
            _currentAmmoWorkersInTurretStackArae.Enqueue(ammoWorkerBrain);

            if (_currentAmmoWorkersInTurretStackArae.Count != 0)
            _selectedTarget.AddStack(turretStackGridController.LastPosition(), _currentAmmoWorkersInTurretStackArae.Peek()
                .GetComponent<AmmoWorkerStackController>().SendAmmoStack());

            ammoWorkerBrain.IsLoadTurret(true);
        }



        internal void WhenExitTurretStack(AmmoWorkerBrain ammoWorkerBrain) => 
            ammoWorkerBrain.IsLoadTurret(false);

        public void WhenExitOnTurretStack(AmmoWorkerStackController ammoWorkerStackController) =>
            ammoWorkerStackController.SetClearWorkerStackList();//clear

        internal void WhenAmmoworkerEnterAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain) =>
            ammoWorkerBrain.SetTriggerInfo(true);

        internal void WhenAmmoworkerExitAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain) =>
            ammoWorkerBrain.SetTriggerInfo(false);

        internal void WhenStayOnAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain,AmmoWorkerStackController ammoWorkerStackController)//clear
        {           
            if (counter < _ammoWorkerAIData.MaxStackCount)
            {
                ammoWorkerStackController.AddStack(_ammoWorkerAIData.AmmoWareHouse, ammoWorkerBrain.gameObject.transform, 
                                          GetObject(PoolType.Ammo.ToString()));
                counter++;
            }
            else
            {
                ammoWorkerBrain.ChangeAmmoWorkerStackStatus(AmmoStackStatus.Full);
            }
        }

        public GameObject GetObject(string poolName) => ObjectPoolManager.Instance.GetObject<GameObject>(poolName);

        private void OnPlayerEnterAmmoWorkerCreaterArea(Transform workerCreater)
        {
            AddAmmaWorker(workerCreater);
        }

        public void AddAmmaWorker(Transform workerCreater)
        {
            GameObject ammoWorker = GetObject(PoolType.AmmoWorkerAI.ToString());

            ammoWorker.transform.position = workerCreater.position;
        }
      
        public void ResetItems() => counter = 0;

    }
}