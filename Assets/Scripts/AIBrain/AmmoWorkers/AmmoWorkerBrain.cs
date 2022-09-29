using Abstraction;
using Data.UnityObject;
using Datas.ValueObject;
using Managers;
using StateBehavior;
using States;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrain
{
    public class AmmoWorkerBrain : MonoBehaviour
    {
        #region Self Variables

        #region SerilizeField Variables
        [SerializeField]
        private CD_AIData cD_AIData;
        [SerializeField]
        private NavMeshAgent _agent;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private AmmoManager ammoManager;
        #endregion

        #region Private Variables 

        int counter;
        [SerializeField]
        private bool  _inplaceWorker=false;

        private GameObject _targetTurretContayner;

 
        #endregion

        #region State Field

        private MoveToWareHouse _moveToWareHouse;
        private TakeAmmo _takeAmmo;

        private MoveToAvaliableContayner _moveToAvaliableConteyner;
        private LoadContayner _loadTurret;
        private FullAmmo _fullAmmo;
        private Creat _creat;
        private AmmoWorkerAIData _ammoWorkerAIData;


       

        private StateMachine _statemachine;

        #endregion
        #endregion

        #region GetReferans
        internal  void Awake()
        {
           
            Init();
  
        }

        private void Init()
        {
            _ammoWorkerAIData = cD_AIData.AmmoWorkerAIDatas;
            GetStatesReferences();
        }

        public void SetTriggerInfo(bool IsInPlaceWareHouse) => _inplaceWorker = IsInPlaceWareHouse;

        public void SetTargetTurretContayner(GameObject targetTurretContayner)
        {

            _moveToAvaliableConteyner.SetData(targetTurretContayner);

            _targetTurretContayner = targetTurretContayner;
        }
        internal  void GetStatesReferences()
        {
            _statemachine = new StateMachine();

            _creat = new Creat();

            _moveToWareHouse = new MoveToWareHouse(_agent, _animator, _ammoWorkerAIData.MovementSpeed, _ammoWorkerAIData.AmmoWareHouse, _ammoWorkerAIData.AmmoWorker,this);

            _takeAmmo = new TakeAmmo(_agent, _animator, _ammoWorkerAIData.AmmoWareHouse, _ammoWorkerAIData.MaxStackCount,_ammoWorkerAIData.AmmoWorker.transform,this);

            _moveToAvaliableConteyner = new MoveToAvaliableContayner(_agent, _animator, _ammoWorkerAIData.MovementSpeed);

            _loadTurret = new LoadContayner(_agent, _animator, _ammoWorkerAIData.MovementSpeed, _ammoWorkerAIData.AmmoWareHouse);

            _fullAmmo = new FullAmmo(_agent, _animator, _ammoWorkerAIData.MovementSpeed);

            TransitionofState();
        }


        #endregion

        #region StateEngine

        internal  void TransitionofState()
        {
   

            #region Transtion

            At(_creat, _moveToWareHouse, IsAmmoWorkerBorn());

            At(_moveToWareHouse, _takeAmmo, WhenAmmoWorkerInAmmoWareHouse());

            At(_takeAmmo, _moveToAvaliableConteyner,WhenAmmoWorkerStackFull());

            At(_moveToAvaliableConteyner, _loadTurret, IsAmmoWorkerInContayner());

            At(_loadTurret, _moveToWareHouse, WhenAmmoDichargeStack());

            if (_takeAmmo.IsStackFull() == Enums.PlayerAmmaStackStatus.Full)
            {
                _statemachine.AddAnyTransition(_fullAmmo, HasNoEmtyTarget());//bak buna 
            }

            _statemachine.SetState(_creat);

            void At(IState to, IState from, Func<bool> condition) => _statemachine.AddTransition(to, from, condition);

            #endregion

            #region Conditions

            Func<bool> IsAmmoWorkerBorn() => () => _ammoWorkerAIData.AmmoWareHouse.transform != null;

            Func<bool> WhenAmmoWorkerInAmmoWareHouse() => () => _inplaceWorker == true && _ammoWorkerAIData.AmmoWareHouse.transform != null;

            Func<bool> WhenAmmoWorkerStackFull() => () => _targetTurretContayner != null && _takeAmmo.IsStackFull() == Enums.PlayerAmmaStackStatus.Full;

            Func<bool> IsAmmoWorkerInContayner() => () => _targetTurretContayner != null;

            Func<bool> WhenAmmoDichargeStack() => () => _ammoWorkerAIData.AmmoWareHouse.transform != null;

            Func<bool> HasNoEmtyTarget() => () => _targetTurretContayner == null;

            #endregion
        }

        public void Update()
        {
            Debug.Log(_inplaceWorker);
            _statemachine.Tick();
        }
        #endregion



    }
}
