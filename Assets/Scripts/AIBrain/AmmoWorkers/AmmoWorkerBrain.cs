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

        #endregion

        #region Private Variables 

        private bool _IsAmmoWorkerReachedWareHouse;
        private bool _isLoadTurretContayner;
        private GameObject _targetAmmoDropZone;

 
        #endregion

        #region State Field

        private MoveToWareHouse _moveToWareHouse;
        private TakeAmmo _takeAmmo;

        private MoveToAvaliableContayner _moveToAvaliableAmmoDropZone;

        private AmmoStackStatus _ammoWorkerStackStatus;

        private LoadContayner _loadTurret;
        private FullAmmo _fullAmmo;
        private Create _creat;
        private AmmoWorkerAIData _ammoWorkerAIData;

        private StateMachine _statemachine;

        private bool _isAmmoWorkerReachedWareHouse;

        public bool IsAmmoWorkerReachedWareHouse 
        {
            get => _isAmmoWorkerReachedWareHouse; 
            set => _isAmmoWorkerReachedWareHouse = value; 
        }



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
        public void ChangeAmmoWorkerStackStatus(AmmoStackStatus status) => _ammoWorkerStackStatus = status;

        // public void SetTriggerInfo(bool IsInPlaceWareHouse) //=> //_IsAmmoWorkerReachedWareHouse = IsInPlaceWareHouse;


        // public void IsLoadTurret(bool isLoadTurretContayner)// => _isLoadTurretContayner = isLoadTurretContayner;

        public void SetTargetTurretStack(GameObject targetTurretContayner)
        {

            if (targetTurretContayner == null) Debug.LogError("Turret Stack Target Is Null In AmmoWorkerBrain");

            _moveToAvaliableAmmoDropZone.SetData(targetTurretContayner);
            _targetAmmoDropZone = targetTurretContayner;
        }

        public  void GetStatesReferences()
        {
            _statemachine = new StateMachine();

            _creat = new Create();

            _moveToWareHouse = new MoveToWareHouse(_agent, _animator, _ammoWorkerAIData.MovementSpeed,_ammoWorkerAIData.AmmoWareHouse);

            _takeAmmo = new TakeAmmo(_agent,_animator);

            _moveToAvaliableAmmoDropZone = new MoveToAvaliableContayner(_agent, _animator, _ammoWorkerAIData.MovementSpeed);

            _loadTurret = new LoadContayner(_agent, _animator, _ammoWorkerAIData.MovementSpeed, _ammoWorkerAIData.AmmoWareHouse);

            _fullAmmo = new FullAmmo(_agent, _animator, _ammoWorkerAIData.MovementSpeed);

        }



        #endregion

        #region StateEngine

        public void TransitionofState()
        {
   

            #region Transtion

            At(_creat, _moveToWareHouse,()=> true);

            At(_moveToWareHouse, _takeAmmo, WhenAmmoWorkerInAmmoWareHouse());

            At(_takeAmmo, _moveToAvaliableAmmoDropZone,WhenAmmoWorkerStackFull());

            At(_moveToAvaliableAmmoDropZone, _loadTurret, IsAmmoWorkerInContayner());

            At(_loadTurret, _moveToWareHouse, WhenAmmoDichargeStack());

            _statemachine.SetState(_creat);

            void At(IState to, IState from, Func<bool> condition) => _statemachine.AddTransition(to, from, condition);

            #endregion

            #region Conditions

            Func<bool> WhenAmmoWorkerInAmmoWareHouse() => () => _IsAmmoWorkerReachedWareHouse == true;

            Func<bool> WhenAmmoWorkerStackFull() => () => _targetAmmoDropZone != null;

            Func<bool> IsAmmoWorkerInContayner() => () => _targetAmmoDropZone != null && _isLoadTurretContayner==true;

            Func<bool> WhenAmmoDichargeStack() => () =>  _ammoWorkerStackStatus == AmmoStackStatus.Empty;


            #endregion
        }

        public void Update()
        {

            _statemachine.Tick();
        }
        #endregion



    }
}
