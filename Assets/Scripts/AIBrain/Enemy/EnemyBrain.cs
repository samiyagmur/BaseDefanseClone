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

namespace AIBrain
{
    public class EnemyBrain :MonoBehaviour
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
        private EnemyTypeData _enemyData;
        private EnemyType _enemyType;
        private Transform _playerTarget;
        private float _navMeshRadius;
        private float _playerDamage;//not ready
        private float _navMeshHeigh;
        private Color _bodyColor;
        private float _chaseSpeed;
        private Vector3 _scaleSize;
        private Transform _mineTarget;
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
        public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
        public Transform PlayerTarget { get => _playerTarget; set => _playerTarget = value; }
        public Transform MineTarget { get => _mineTarget; set => _mineTarget = value; }
        public int Healt { get => _healt; set => _healt = value; }
        public EnemyType EnemyType { get => _enemyType; set => _enemyType = value; }
        #endregion

        #region Get&SetData

        private void Awake()
        {
            _enemyData = GetData();
            SetEnemyData();
            GetStatesReferences();


        }

        private EnemyTypeData GetData() => Resources.Load<CD_AIData>("Data/CD_AIData").enemy.EnemyList[(int)EnemyType];

        private void SetEnemyData()
        {
            _healt = _enemyData.Healt;
            _damage = _enemyData.Damage;
            _attackRange = _enemyData.AttackRange;
            _moveSpeed = _enemyData.MoveSpeed;
            _turretTarget = _enemyData.TurretList[Random.Range(0, _enemyData.TurretList.Count)];
            _spawnPosition = _enemyData.SpawnPosition;
            _navMeshRadius = _enemyData.NavMeshRadius;
            _navMeshHeigh = _enemyData.NavMeshHeigh;
            EnemyType = _enemyData.EnemyType;
            _bodyColor = _enemyData.color;
            _chaseSpeed = _enemyData.ChaseSpeed;
            _scaleSize = _enemyData.scaleSize;

            
        }


        #endregion
        private void GetStatesReferences()
        {
            _stateMachine = new StateMachine();
            
            Search _search = new Search(_animator, _navMeshAgent, this, _spawnPosition);
            Move _move = new Move(_animator, _navMeshAgent, this, MoveSpeed, _turretTarget);
            Chase _chase = new Chase(_animator,_navMeshAgent,this,MoveSpeed,_attackRange);///physic controllerdan player gelcek
            Attack _atack = new Attack(_animator, _navMeshAgent, this, PlayerTarget, _attackRange);
            Death _death = new Death(_animator,_navMeshAgent,this);//Listeli bir yapý düsün

           BoombManager moveToBomb = new BoombManager(_navMeshAgent, _animator, this, _attackRange, _chaseSpeed);

            TransitionofState(_search, _move, _chase, _atack, _death, moveToBomb);
        }

        private void TransitionofState(Search search, Move move, Chase chase, Attack attack, Death death, BoombManager moveToBomb)
        {
           
                
            At(search, move, HasTurretTarget()); // player chase range
            At(move, chase, HasTarget()); // player chase range
            At(chase, attack, IsAtackPlayer()); // remaining distance < 1f
            At(attack, chase, AttackOffRange()); // remaining distance > 1f
            At(chase, move, HasTargetNull());


            _stateMachine.AddAnyTransition(death, () => enemyPhysicsController.AmIDead);
             _stateMachine.AddAnyTransition(moveToBomb, () => enemyPhysicsController.IsBombInRange());

            _stateMachine.SetState(search);

            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> HasTurretTarget() => () => _turretTarget != null;
            Func<bool> HasTarget() => () => PlayerTarget != null;
            Func<bool> HasTargetNull() => () => PlayerTarget is null;
            Func<bool> IsAtackPlayer() => () => PlayerTarget != null && chase.GetPlayerInRange();
            Func<bool> AttackOffRange() => () =>!attack.InPlayerAttackRange();
        }

        private void Update()
        {
            Debug.Log(enemyPhysicsController.AmIDead);

            _stateMachine.Tick();
        
        }        
    }
}
