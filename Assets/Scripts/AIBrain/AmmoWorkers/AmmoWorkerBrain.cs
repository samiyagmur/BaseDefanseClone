using Abstraction;
using Controllers;
using Data.UnityObject;
using Datas.ValueObject;
using StateBehavior;
using States;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrain
{
    public class AmmoWorkerBrain : StateUsers
    {

        #region Self Variables
        #region SerilizeField Variables

        #endregion

        #region private Variables 
        private AmmoWorkerAIData _ammoWorkerAIData;
        private MoveToAmmo _moveToAmmo;
        private TakeAmmo _takeAmmo;
        private DecideAvaliableTurret _decideAvaliableTurret;
        private MoveToAvaliableTurret _moveToAvaliableTurret;
        private LoadTurret _loadTurret;
        private float _offset;
        private float _stackHeigh;
        private float _stackwidth;
        private float _movementSpeed;
        private Transform _spawnPoint;
        private Transform _ammoStore;
        private List<Transform> _ammoContainer;
        private StateMachine _statemachine;
        #endregion

        #endregion
        [SerializeField]
        private NavMeshAgent _navMeshAgent;
        [SerializeField]
        private Animator _animator;

        internal override void Awake()
        {
            GetStatesReferences();
        }
        internal override void GetData() => _ammoWorkerAIData=Resources.Load<CD_AIData>("Data/CD_AIData").AmmoWorkerAIDatas;
        internal override void GetStatesReferences()
        {
            _statemachine = new StateMachine();
            _moveToAmmo = new MoveToAmmo();
            _takeAmmo = new TakeAmmo();
            _decideAvaliableTurret = new DecideAvaliableTurret();
            _moveToAvaliableTurret = new MoveToAvaliableTurret();
            _loadTurret = new LoadTurret();

        }

        internal override void SetEnemyAIData()
        {
            float _offset= _ammoWorkerAIData.Offset;
            float _stackHeigh= _ammoWorkerAIData.StackHeigh;
            float _stackwidth= _ammoWorkerAIData.Stackwidth;
            float _movementSpeed= _ammoWorkerAIData.MovementSpeed;
            Transform _spawnPoint= _ammoWorkerAIData.SpawnPoint;
            Transform _ammoStore= _ammoWorkerAIData.AmmoStore;
            List<Transform> _ammoContainer= _ammoWorkerAIData.AmmoContainer;
        }

        internal override void TransitionofState()
        {
            _statemachine.SetState(_moveToAmmo);

            At(_moveToAmmo, _takeAmmo, HasAmmoStore());
            At(_takeAmmo,_decideAvaliableTurret, HasTarget())



            Func<bool> HasAmmoStore() => () => _ammoStore != null;
            Func<bool> HasTarget() => () => 
            Func<bool> HasTargetNull() => () => 
            Func<bool> IsAtackPlayer() => () => 
            Func<bool> AttackOffRange() => () => 


            void At(IState to, IState from, Func<bool> condition) => _statemachine.AddTransition(to, from, condition);
        }





        internal override void Update() => _statemachine.Tick();


    }
}
