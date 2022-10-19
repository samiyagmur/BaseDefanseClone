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

       

        List<TurretStackController> turretStackController=new List<TurretStackController>();

        List<TurretStackController> selectTargetList = new List<TurretStackController>();
        [SerializeField]
        Queue<AmmoWorkerBrain> _currentAmmoWorkersInTurretStackArae=new Queue<AmmoWorkerBrain>();

        #endregion

        private int counter;
        private TurretKey turretKey;
        private AmmoWorkerAIData _ammoWorkerAIData;
        private TurretStackGridController turretStackGridController;

  
        private TurretStackController _selectedTarget;

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
            //AmmoManagerSignals.Instance.onSetAmmoStackStatus += OnSetAmmoStackStatus;
            AmmoManagerSignals.Instance.onGetAmmoToFire -= OnGetAmmoToFire;
            AmmoManagerSignals.Instance.onSetTurretStackControllers += OnSetTurretStackControllers;

        }

        private void UnsubscribeEvents()
        {
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea -= OnPlayerEnterAmmoWorkerCreaterArea;
            //AmmoManagerSignals.Instance.onSetAmmoStackStatus -= OnSetAmmoStackStatus;
            AmmoManagerSignals.Instance.onGetAmmoToFire -= OnGetAmmoToFire;
            AmmoManagerSignals.Instance.onSetTurretStackControllers -= OnSetTurretStackControllers;
        }
        private GameObject OnGetAmmoToFire(TurretKey arg)
        {
            return null;
        }

        private void OnSetTurretStackControllers(TurretStackController stack)
        {
            turretStackController.Add(stack);
        }

        internal void WhenGetTurretStackInfo(AmmoWorkerBrain ammoWorkerBrain)
        {
            if (selectTargetList.Count == 0)
            {
                selectTargetList = turretStackController.OrderBy(x => x.GetCurrentCount()).ToList();
            }

            _selectedTarget = selectTargetList[0];

            turretStackController.RemoveAt(0);

            selectTargetList.TrimExcess();

            selectTargetList.RemoveAt(0);

            selectTargetList.TrimExcess();

            ammoWorkerBrain.SetTargetTurretStack(_selectedTarget.gameObject);

        }

        internal void WhenEnterTurretStack(TurretStackController turretStackController, AmmoWorkerStackController ammoWorkerStackController)
        {
            turretStackController.AddStack(turretStackGridController.LastPosition(), ammoWorkerStackController.SendAmmoStack());

        }

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

        internal void WhenEnterTurretStack(AmmoWorkerBrain ammoWorkerBrain)
            => ammoWorkerBrain.IsLoadTurret(true);

        internal void WhenExitTurretStack(AmmoWorkerBrain ammoWorkerBrain)
            => ammoWorkerBrain.IsLoadTurret(false);

        public void WhenExitOnTurretStack(AmmoWorkerStackController ammoWorkerStackController) 
            => ammoWorkerStackController.SetClearWorkerStackList();//clear

        internal void WhenAmmoworkerEnterAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain) 
            =>  ammoWorkerBrain.SetTriggerInfo(true);

        internal void WhenAmmoworkerExitAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain) 
            => ammoWorkerBrain.SetTriggerInfo(false);

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


//internal void GiveTargetInAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain)
//{
//    //turretStackController = turretStackController[];//debam



//    if (selectTargetList.Count == 0)
//    {
//        selectTargetList = turretStackController.OrderBy(x => x.GetCurrentCount()).ToList();
//    }

//    _selectedTarget = selectTargetList[0];

//    selectTargetList.RemoveAt(0);

//    selectTargetList.TrimExcess();

//    ammoWorkerBrain.SetTargetTurretStack(_selectedTarget.gameObject);
//}