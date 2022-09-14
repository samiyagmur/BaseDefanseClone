using Abstraction;
using AIBrain.Enemy.State;
using Assets.Scripts.Abstraction;
using Datas.UnityObject;
using Enums;
using StateBehavior;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

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
        private EnemyType enemyType;
        private Transform _playerTarget;
        private float _playerDamage;//not ready


        #endregion

        #region SerializeField Variables

        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        [SerializeField]
        private Animator _animator;

        #endregion

        #endregion
        public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
        public Transform PlayerTarget { get => _playerTarget; set => _playerTarget = value; }

        #region Get&SetData

        private void Awake()
        {

            _enemyData = GetData();
            SetEnemyData();
            GetStatesReferences();
            //Debug.Log(_turretTarget.name);
        }

        private EnemyTypeData GetData() => Resources.Load<CD_AIData>("Data/CD_AIData").enemy.EnemyList[(int)enemyType];

        private void SetEnemyData()
        {
            _healt = _enemyData.Healt;
            _damage = _enemyData.Damage;
            _attackRange = _enemyData.AttackRange;
            MoveSpeed = _enemyData.MoveSpeed;
            _turretTarget = _enemyData.TurretList[Random.Range(0, _enemyData.TurretList.Count)];
            _spawnPosition = _enemyData.SpawnPosition;
        }


        #endregion
        private void GetStatesReferences()
        {

            _stateMachine = new StateMachine();

            Search _search = new Search(_animator, _navMeshAgent, this, _spawnPosition);
            Move _move = new Move(_animator, _navMeshAgent, this, MoveSpeed, _turretTarget);
            Chase _chase = new Chase(_animator,_navMeshAgent,this,MoveSpeed,_attackRange);///physic controllerdan player gelcek
            Attack _atack = new Attack(_animator, _navMeshAgent, this,_playerTarget, _attackRange);
            Death _death = new Death();//Listeli bir yapý düsün

            TransitionofState(_search, _move, _chase, _atack, _death);

        }

        private void TransitionofState(Search search, Move move, Chase chase, Attack attack, Death death)
        {
            At(search, move, HasTurretTarget()); // player chase range
            At(move, chase, HasTarget()); // player chase range
            At(chase, attack, IsAtackPlayer()); // remaining distance < 1f
            At(attack, chase, AttackOffRange()); // remaining distance > 1f
            At(chase, move, HasTargetNull());

            _stateMachine.SetState(search);
            //_stateMachine.AddAnyTransition(death, () => death.isDead);
            //_stateMachine.AddAnyTransition(move, () => death.isDead);
            //At(moveToBomb, attack, () => moveToBomb.BombIsAlive);

            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> HasTurretTarget() => () => _turretTarget != null;
            Func<bool> HasTarget() => () => PlayerTarget != null;
            Func<bool> HasTargetNull() => () => PlayerTarget is null;
            Func<bool> IsAtackPlayer() => () => PlayerTarget != null && chase.GetPlayerInRange();
            Func<bool> AttackOffRange() => () => !attack.InPlayerAttackRange();
        }

        private void Update()
        {
            _stateMachine.Tick(); 
        
        } 

        //public void SetPlayerTarget(Transform target)
        //{
        //    PlayerTarget = target;
            

        //} 
       
    }
}
