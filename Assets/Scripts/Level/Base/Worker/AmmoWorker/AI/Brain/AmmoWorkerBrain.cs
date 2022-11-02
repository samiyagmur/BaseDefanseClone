using Abstraction;
using AI.States;
using Data.UnityObject;
using Datas.ValueObject;
using Enums;
using Interfaces;
using Managers;
using StateBehavior;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrain.AmmoWorkers
{
    public class AmmoWorkerBrain : MonoBehaviour
    {
        #region Self Variables

        #region SerilizeField Variables

        [SerializeField]
        private NavMeshAgent agent;
        [SerializeField]
        private Animator animator;

        #endregion

        #region Private Variables 

        private bool _inplaceWorker;
        private bool _isLoadTurretContayner;
        private GameObject _targetTurretContayner;

 
        #endregion

        #region State Field

        private MoveToWareHouse _moveToWareHouse;
        private TakeAmmo _takeAmmo;

        private MoveToAvaliableContayner _moveToAvaliableConteyner;

        private AmmoStackStatus _playerAmmaStackStatus;

        private LoadContayner _loadTurret;
        private FullAmmo _fullAmmo;
        private Create _creat;
        private AmmoWorkerAIData _ammoWorkerAIData;
        private float _speed;
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
            _speed = _ammoWorkerAIData.MovementSpeed;
            GetStatesReferences();
            TransitionofState();
        }
        public void ChangeAmmoWorkerStackStatus(AmmoStackStatus status) => _playerAmmaStackStatus = status;

        public void SetTriggerInfo(bool IsInPlaceWareHouse) => _inplaceWorker = IsInPlaceWareHouse;
        internal void IncreaseSpeed(float amount)
        {
            //_speed += amount;
            _moveToWareHouse.IncreaseSpeed(_speed);
            _moveToAvaliableConteyner.IncreaseSpeed(_speed);
            _fullAmmo.IncreaseSpeed(_speed);
        }

        internal bool Get_isLoadTurretContayner() => _isLoadTurretContayner;

        internal void IsLoadTurret(bool isLoadTurretContayner) => _isLoadTurretContayner = isLoadTurretContayner;

        public void SetTargetTurretStack(GameObject targetTurretContayner)
        {

            if (targetTurretContayner == null) Debug.LogError("Turret Stack Target Is Null In AmmoWorkerBrain");

            _moveToAvaliableConteyner.SetData(targetTurretContayner);
            _targetTurretContayner = targetTurretContayner;
        }

        internal  void GetStatesReferences()
        {
            _statemachine = new StateMachine();

            _creat = new Create();

            _moveToWareHouse = new MoveToWareHouse(agent, animator, _speed, 
                                                    _ammoWorkerAIData.AmmoWareHouse, _ammoWorkerAIData.AmmoWorker,this);

            _takeAmmo = new TakeAmmo(agent,animator);

            _moveToAvaliableConteyner = new MoveToAvaliableContayner(agent, animator, _speed);

            _loadTurret = new LoadContayner(agent, animator, _ammoWorkerAIData.AmmoWareHouse);

            _fullAmmo = new FullAmmo(agent, animator, _speed, _ammoWorkerAIData.AmmoWareHouse);

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

            At(_fullAmmo, _moveToWareHouse, WhenAmmoWorkerStackFull());

            _statemachine.AddAnyTransition(_fullAmmo, HasNoEmtyTarget());//bak buna 


            _statemachine.SetState(_creat);

            void At(IState to, IState from, Func<bool> condition) => _statemachine.AddTransition(to, from, condition);

            #endregion

            #region Conditions

            Func<bool> IsAmmoWorkerBorn() => () => _ammoWorkerAIData.AmmoWareHouse.transform != null;

            Func<bool> WhenAmmoWorkerInAmmoWareHouse() => () => _inplaceWorker == true && _ammoWorkerAIData.AmmoWareHouse.transform != null;

            Func<bool> WhenAmmoWorkerStackFull() => () => _targetTurretContayner != null && _playerAmmaStackStatus == AmmoStackStatus.Fill;

            Func<bool> IsAmmoWorkerInContayner() => () => _targetTurretContayner != null && _isLoadTurretContayner==true;

            Func<bool> WhenAmmoDichargeStack() => () =>  _playerAmmaStackStatus == AmmoStackStatus.Empty;

            Func<bool> HasNoEmtyTarget() => () => _playerAmmaStackStatus==AmmoStackStatus.Full;

            #endregion
        }

        public void Update()
        {

            _statemachine.Tick();
        }


        #endregion



    }
}
