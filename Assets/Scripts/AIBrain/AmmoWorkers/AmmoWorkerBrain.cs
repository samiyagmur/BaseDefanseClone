using Abstraction;
using Data.UnityObject;
using Datas.ValueObject;
using Enums;
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
        private NavMeshAgent _agent;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private AmmoManager ammoManager;
        #endregion

        #region Private Variables 

        int counter;
        [SerializeField]
        private bool  _inplaceWorker;
        private bool _isLoadTurretContayner;
        private GameObject _targetTurretContayner;

 
        #endregion

        #region State Field

        private MoveToWareHouse _moveToWareHouse;
        private TakeAmmo _takeAmmo;

        private MoveToAvaliableContayner _moveToAvaliableConteyner;

        private PlayerAmmaStackStatus _playerAmmaStackStatus;

        internal void IsLoadTurret(bool isLoadTurretContayner)
        {
            _isLoadTurretContayner=isLoadTurretContayner;
        }

        private LoadContayner _loadTurret;
        private FullAmmo _fullAmmo;
        private Creat _creat;
        private AmmoWorkerAIData _ammoWorkerAIData;


       

        private StateMachine _statemachine;

        #endregion
        #endregion

        #region GetReferans
        private void Awake()
        {
            InitBrain();
        }
        public void InitBrain()
        {
            _ammoWorkerAIData = Resources.Load<CD_AIData>("Data/CD_AIData").AmmoWorkerAIDatas;
            GetStatesReferences();
            TransitionofState();
        }
        public void IsStackFul(PlayerAmmaStackStatus status) => _playerAmmaStackStatus = status;

        public void SetTriggerInfo(bool IsInPlaceWareHouse) => _inplaceWorker = IsInPlaceWareHouse;

        public void SetTargetTurretContayner(GameObject targetTurretContayner)
        {
            if (targetTurretContayner == null) return;

            _moveToAvaliableConteyner.SetData(targetTurretContayner);

            _targetTurretContayner = targetTurretContayner;
        }

        internal  void GetStatesReferences()
        {
            _statemachine = new StateMachine();

            _creat = new Creat();

            _moveToWareHouse = new MoveToWareHouse(_agent, _animator, _ammoWorkerAIData.MovementSpeed, _ammoWorkerAIData.AmmoWareHouse, _ammoWorkerAIData.AmmoWorker,this);

            _takeAmmo = new TakeAmmo(_agent,_animator);

            _moveToAvaliableConteyner = new MoveToAvaliableContayner(_agent, _animator, _ammoWorkerAIData.MovementSpeed);

            _loadTurret = new LoadContayner(_agent, _animator, _ammoWorkerAIData.MovementSpeed, _ammoWorkerAIData.AmmoWareHouse);

            _fullAmmo = new FullAmmo(_agent, _animator, _ammoWorkerAIData.MovementSpeed);

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

            if (_playerAmmaStackStatus == PlayerAmmaStackStatus.Full)
            {
                _statemachine.AddAnyTransition(_fullAmmo, HasNoEmtyTarget());//bak buna 
            }

            _statemachine.SetState(_creat);

            void At(IState to, IState from, Func<bool> condition) => _statemachine.AddTransition(to, from, condition);

            #endregion

            #region Conditions

            Func<bool> IsAmmoWorkerBorn() => () => _ammoWorkerAIData.AmmoWareHouse.transform != null;

            Func<bool> WhenAmmoWorkerInAmmoWareHouse() => () => _inplaceWorker == true && _ammoWorkerAIData.AmmoWareHouse.transform != null;

            Func<bool> WhenAmmoWorkerStackFull() => () => _targetTurretContayner != null && _playerAmmaStackStatus == PlayerAmmaStackStatus.Full;

            Func<bool> IsAmmoWorkerInContayner() => () => _targetTurretContayner != null && _isLoadTurretContayner==true;

            Func<bool> WhenAmmoDichargeStack() => () =>  _playerAmmaStackStatus == PlayerAmmaStackStatus.Empty;

            Func<bool> HasNoEmtyTarget() => () => _targetTurretContayner == null;

            #endregion
        }

        public void Update()
        {

            _statemachine.Tick();
        }
        #endregion



    }
}
