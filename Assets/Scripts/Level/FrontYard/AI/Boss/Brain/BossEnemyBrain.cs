using Assets.Scripts;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Interfaces;
using Sirenix.OdinInspector;
using StateBehavior;
using StateMachines.AIBrain.Enemy.States;
using System;
using UnityEngine;

namespace StateMachines.AIBrain.Enemy
{
    public class BossEnemyBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [BoxGroup("Public Variables")]
        public Transform PlayerTarget;

        [BoxGroup("Public Variables")]
        public int Health;

        #endregion Public Variables

        #region Serilizable Variables

        [BoxGroup("Serializable Variables")]
        [SerializeField]
        private EnemyType enemyType;

        [BoxGroup("Serializable Variables")]
        [SerializeField]
        private BossEnemyDetector detector;

        [BoxGroup("Serializable Variables")]
        [SerializeField]
        private Transform bombHolder;

        [SerializeField]
        private BossHealtController bossHealtController;

        #endregion Serilizable Variables

        #region Private Variables

        private EnemyData _enemyData;
        private EnemyAIData _enemyAIData;
        private StateMachine _stateMachine;
        private Animator _animator;

        #region States

        private BossWaitState _waitState;
        private BossAttackState _attackState;
        private BossDeathState _deathState;

        #endregion States

        #endregion Private Variables

        #endregion Self Variables

        private void Awake()
        {
            _enemyAIData = GetAIData();
            _enemyData = GetEnemyType();
            SetEnemyVariables();
            GetReferenceStates();
        }

        private void SetEnemyVariables()
        {
            _animator = GetComponentInChildren<Animator>();
            Health = _enemyData.Healt;
            bossHealtController.SetHealth(_enemyData.Healt);
        }

        #region Data Jobs

        private EnemyData GetEnemyType()
        {
            return _enemyAIData.EnemyDatas[(int)enemyType];
        }

        private EnemyAIData GetAIData()
        {
            return Resources.Load<CD_AIData>("Data/CD_AIData").EnemyAIData;
        }

        #endregion Data Jobs

        #region AI State Jobs

        private void GetReferenceStates()
        {
            _waitState = new BossWaitState(_animator, this);
            _attackState = new BossAttackState(_animator, this, _enemyData.AttackRange, ref bombHolder);
            _deathState = new BossDeathState(_animator, this, enemyType);

            //Statemachine statelerden sonra tanimlanmali ?
            _stateMachine = new StateMachine();

            At(_waitState, _attackState, IAttackPlayer());
            At(_attackState, _waitState, INoAttackPlayer());

            _stateMachine.AddAnyTransition(_deathState, AmIDead());
            //SetState state durumlari belirlendikten sonra default deger cagirilmali
            _stateMachine.SetState(_waitState);

            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
            Func<bool> IAttackPlayer() => () => PlayerTarget != null;
            Func<bool> INoAttackPlayer() => () => PlayerTarget == null;
            Func<bool> AmIDead() => () => Health <= 0;
        }

        #endregion AI State Jobs

        private void Update() => _stateMachine.Tick();

        [Button]
        private void BossDeathState()
        {
            Health = 0;
        }
    }
}