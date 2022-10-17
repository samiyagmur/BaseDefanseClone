using Enums;
using StateBehavior;
using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Controllers;
using Data.ValueObject;
using Data.UnityObject;
using AI.States;
using Interfaces;


namespace AIBrain.EnemyBrain
{
    public class EnemyBrain : MonoBehaviour
    {

        #region SelfVariables

        #region Private instanceses
        private StateMachine _stateMachines;
        private Search _search;
        private Move _move;
        private Chase _chase;
        private Atack _atack;
        private Death _death;
        private BoombManager _moveToBomb;
        private Transform  _turretTarget;
        private Transform _turretTargetList;
        private Transform _spawnPosition;
        private EnemyType _enemyTypes;
        public int _health;
        private int _damage;
        private float _attackRange;
        private float _moveSpeed;
        private float _navMeshRadius;
        private float _enemyDamage;
        private Color _color;
        private float _chaseSpeed;
        private Vector3 _scaleSize;
        private Transform _playerTarget;
        private EnemyAIData _enemyAIData;
        private Transform _mineTarget;//bu niye var
        #endregion

        #region SerializeField Variables

        [SerializeField]
        private NavMeshAgent _navMeshAgents;
        [SerializeField]
        private Animator _animators;
        [SerializeField]
        private EnemyDetectController _enemyPhysicsController;
        #endregion
        #endregion

        #region Proporties
        public Transform PlayerTarget { get => _playerTarget; set => _playerTarget = value; }
        public EnemyType EnemyTypes { get => _enemyTypes; set => _enemyTypes = value; }
        public Transform MineTarget { get => _mineTarget; set => _mineTarget = value; }
        #endregion

        internal  void Awake()
        {
            GetData();
            SetEnemyAIData();
            GetStatesReferences();

        }
        internal  void GetData() => _enemyAIData = Resources.Load<CD_AIData>("Data/CD_AIData").EnemyAIDataList[(int)EnemyTypes];

        internal  void SetEnemyAIData()
        {
            int turretCount = Random.Range(0, _enemyAIData.TurretTargetList.Count);
            _turretTarget = _enemyAIData.TurretTargetList[Random.Range(0, turretCount)];
            _spawnPosition = _enemyAIData.SpawnPosition;
             EnemyTypes = _enemyAIData.EnemyType;
            _health = _enemyAIData.Healt;
            _damage = _enemyAIData.Damage;
            _attackRange = _enemyAIData.AttackRange;
            _moveSpeed = _enemyAIData.MoveSpeed;
            _navMeshRadius = _enemyAIData.NavMeshRadius;
            _enemyDamage = _enemyAIData.EnemyDamage;
            _color = _enemyAIData.Color;
            _chaseSpeed = _enemyAIData.ChaseSpeed;
            _scaleSize = _enemyAIData.ScaleSize;

        }

        internal  void GetStatesReferences()
        {
             _stateMachines = new StateMachine();

             _search = new Search(_animators, _navMeshAgents, this, _spawnPosition);
             _move = new Move(_animators, _navMeshAgents, this, _moveSpeed, _turretTarget);
             _chase = new Chase(_animators, _navMeshAgents, this, _chaseSpeed, _attackRange);
             _atack = new Atack(_animators, _navMeshAgents, this,_attackRange);
             _death = new Death(_animators, _navMeshAgents, this, _enemyTypes);
          //  _moveToBomb = new BoombManager(_navMeshAgents, _animators, this, _attackRange, _chaseSpeed);

            TransitionofState();
        }

        internal  void TransitionofState()
        {
            At(_search, _move, HasTurretTarget()); // player chase range
            At(_move, _chase, HasTarget()); // player chase range
            At(_chase, _atack, IsAtackPlayer()); // remaining distance < 1f
            At(_atack, _chase, AttackOffRange()); // remaining distance > 1f
            At(_chase, _move, HasTargetNull());


            _stateMachines.AddAnyTransition(_death,AmIDead());
            //_stateMachines.AddAnyTransition(_moveToBomb, () => _enemyPhysicsController.IsBombInRange());

            _stateMachines.SetState(_search);

            void At(IState to, IState from, Func<bool> condition) => _stateMachines.AddTransition(to, from, condition);

            Func<bool> HasTurretTarget() => () => _turretTarget != null;
            Func<bool> HasTarget() => () => PlayerTarget != null;
            Func<bool> HasTargetNull() => () => PlayerTarget is null;
            Func<bool> IsAtackPlayer() => () => PlayerTarget != null && _chase.GetPlayerInRange();
            Func<bool> AttackOffRange() => () =>!_atack.InPlayerAttackRange();
            Func<bool> AmIDead() => () => _health <= 0;
        }
        internal  void Update() => _stateMachines.Tick();

        
    }
}
