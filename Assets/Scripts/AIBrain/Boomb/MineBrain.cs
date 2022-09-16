﻿using System;
using Abstraction;
using Boomb;
using Managers;
using StateBehavior;
using UnityEngine;

namespace StateMachines
{
    public class MineBrain : MonoBehaviour
    {

        #region Self Variables
        #region Public Variables
        public MineManager mineManager;
        #endregion
        #region Serialized Variables
        #endregion
        #region Private Variables
        private StateMachine _stateMachine;
        #endregion
        #endregion

        private void Awake()
        {
            GetStatesReferences();
        }

        private void GetStatesReferences()
        {
            var _readyState = new ReadyState();
            var _lureState = new LureState(this);
            var _explosionState = new ExplosionState(this);
            var _mineCountDownState = new MineCountDownState(this);
            _stateMachine = new StateMachine();

            At(_readyState, _lureState, () => mineManager.IsPayedTotalAmount);
            At(_lureState, _explosionState, () => _lureState.IsTimerDone);
            At(_explosionState, _mineCountDownState, () => _explosionState.IsExplosionHappened);
            At(_mineCountDownState, _readyState, () => _mineCountDownState.IsTimerDone);
            _stateMachine.SetState(_readyState);
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        }
        private void Update() => _stateMachine.Tick();
    }
}