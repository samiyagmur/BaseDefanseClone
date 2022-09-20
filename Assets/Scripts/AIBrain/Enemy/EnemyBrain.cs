using Abstraction;
using State;
using Contollers;
using Datas.UnityObject;
using Enums;
using StateBehavior;
using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Managers;
using Data.ValueObject;
using Data.UnityObject;

namespace AIBrain
{
    public class EnemyBrain : StateUsers
    {
        #region SelfVariables

        #region Private Variables

        private Transform _turretTarget;
        private Transform _spawnPosition;
        private int _healt;
        private int _damage;
        private StateMachine _stateMachine;
        private float _attackRange;
        private float _moveSpeed;
        private EnemyAIData _enemyData;
        private EnemyType _enemyType;
        private Transform _playerTarget;
        private float _navMeshRadius;
        private float _playerDamage;
        private float _navMeshHeigh;
        private Color _bodyColor;
        private float _chaseSpeed;
        private Vector3 _scaleSize;
        private Transform _mineTarget;
        private Search _search;
        private Move _move;
        private Chase _chase;
        private Attack _atack;
        private Death _death;
        private BoombManager _moveToBomb;
        #endregion

        #region SerializeField Variables

        [SerializeField]
        EnemyPhysicController enemyPhysicsController;
        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        [SerializeField]
        private Animator _animator;

        #endregion

        #endregion

        #region Proporties
     
        public Transform PlayerTarget { get => _playerTarget; set => _playerTarget = value; }
        public Transform MineTarget { get => _mineTarget; set => _mineTarget = value; }
        public int Healt { get => _healt; set => _healt = value; }
        public EnemyType EnemyType { get => _enemyType; set => _enemyType = value; }
        #endregion

        #region Get&SetData

        internal override  void Awake()
        {
            GetData();
            SetAIData();
            GetStatesReferences();
        }

        internal override void GetData() => _enemyData= Resources.Load<CD_AIData>("Data/CD_AIData").EnemyAIDataList[(int)EnemyType];

        internal override void SetAIData()
        {
            _healt = _enemyData.Healt;
            _damage = _enemyData.Damage;
            _attackRange = _enemyData.AttackRange;
            _moveSpeed = _enemyData.MoveSpeed;
            _turretTarget = _enemyData.TurretTargetList[Random.Range(0, _enemyData.TurretTargetList.Count)];
            _spawnPosition = _enemyData.SpawnPosition;
            _navMeshRadius = _enemyData.NavMeshRadius;
            EnemyType = _enemyData.EnemyType;
            _bodyColor = _enemyData.Color;
            _chaseSpeed = _enemyData.ChaseSpeed;
            _scaleSize = _enemyData.ScaleSize;
        }


        #endregion


        internal override void GetStatesReferences()
        {
            _stateMachine = new StateMachine();
            
             _search = new Search(_animator, _navMeshAgent, this, _spawnPosition);
             _move = new Move(_animator, _navMeshAgent, this, _moveSpeed, _turretTarget);
             _chase = new Chase(_animator,_navMeshAgent,this, _chaseSpeed, _attackRange);
             _atack = new Attack(_animator, _navMeshAgent, this, PlayerTarget, _attackRange);
             _death = new Death(_animator,_navMeshAgent,this);

            _moveToBomb = new BoombManager(_navMeshAgent, _animator, this, _attackRange, _chaseSpeed);

            TransitionofState();
        }

        internal override void TransitionofState()
        {
            At(_search, _move, HasTurretTarget()); // player chase range
            At(_move, _chase, HasTarget()); // player chase range
            At(_chase, _atack, IsAtackPlayer()); // remaining distance < 1f
            At(_atack, _chase, AttackOffRange()); // remaining distance > 1f
            At(_chase, _move, HasTargetNull());


            _stateMachine.AddAnyTransition(_death, () => enemyPhysicsController.AmIDead);
             _stateMachine.AddAnyTransition(_moveToBomb, () => enemyPhysicsController.IsBombInRange());

            _stateMachine.SetState(_search);

            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> HasTurretTarget() => () => _turretTarget != null;
            Func<bool> HasTarget() => () => PlayerTarget != null;
            Func<bool> HasTargetNull() => () => PlayerTarget is null;
            Func<bool> IsAtackPlayer() => () => PlayerTarget != null && _chase.GetPlayerInRange();
            Func<bool> AttackOffRange() => () =>!_atack.InPlayerAttackRange();
        }

        internal override void Update() => _stateMachine.Tick();
      
    }
}
