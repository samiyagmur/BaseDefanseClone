using Abstraction;
using Contollers;
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
        [SerializeField] AmmoWorkerPhysicController AmmoWorkerPhysicsController;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Animator animator;
        #endregion
        #region private Variables
        private AmmoWorkerAIData _ammoWorkerAIData;
        private StateMachine _stateMachine;
        
        private MoveToAmmo _moveToAmmo;

        internal override Transform CreatPoint { get => base.CreatPoint; set => base.CreatPoint = value; }
        internal override float Offset { get => base.Offset; set => base.Offset = value; }
        internal override float StackHeigh { get => base.StackHeigh; set => base.StackHeigh = value; }
        internal override float Stackwidth { get => base.Stackwidth; set => base.Stackwidth = value; }
        internal override float MovementSpeed { get => base.MovementSpeed; set => base.MovementSpeed = value; }
        internal override Transform AmmoStore { get => base.AmmoStore; set => base.AmmoStore = value; }
        internal override Transform AmmoContainer { get => base.AmmoContainer; set => base.AmmoContainer = value; }
        #endregion
        #endregion



        internal override void Awake()
        {
            SetAIData();
            GetStatesReferences();
            
        }

       

        internal override void GetStatesReferences()
        {
            _stateMachine = new StateMachine();
            MoveToAmmo moveToAmmo = new MoveToAmmo();
            
        }

        internal override void SetAIData()
        {

        }

        internal override void TransitionofState()
        {
            _stateMachine.SetState(_moveToAmmo);
            

            



            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);






        }

        internal override void Update() => _stateMachine.Tick();


    }
}
