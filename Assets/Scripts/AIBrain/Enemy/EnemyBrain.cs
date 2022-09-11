using Abstraction;
using AIBrain.Enemy.State;
using Datas.UnityObject;
using Enums;
using StateBehavior;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrain
{
    public class EnemyBrain : MonoBehaviour
    {
        private EnemtTypeData enemtTypeData;
        private StateMachine _stateMachine;
        private Animator _animator;
        private NavMeshAgent _navmeshAgent;
        private int _healt;
        private int _damage;
        private float _attackRange;
        private float _attackSpeed;
        private float _moveSpeed;
        private float _chaseSpeed;
        public List<Transform> TurretTaretList;
        public EnemyType enemyType;

        public Transform Target;

        public NavMeshAgent NavmeshAgent { get => _navmeshAgent; set => _navmeshAgent = value; }

        private void Awake()
        {
            GetReferanceState();
            enemtTypeData = GetData();
            SetAllData();
        }

        private EnemtTypeData GetData()
        {
            return Resources.Load<CD_AIData>("Data/CD_Level").enemy.EnemyList[(int)enemyType];  
        }

        private void SetAllData()
        {
             _healt=enemtTypeData.Healt;
             _damage=enemtTypeData.Damage;
             _attackRange=enemtTypeData.AttackRange;
             _attackSpeed=enemtTypeData.AttackSpeed;
             _moveSpeed=enemtTypeData.MoveSpeed;
             _chaseSpeed=enemtTypeData.ChaseSpeed;
        }



        private void GetReferanceState()
        {
            NavmeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();

            Bomb bomb= new Bomb(NavmeshAgent, _animator);
            Attack attack= new Attack(this,NavmeshAgent,_animator, _attackRange);
            Chase chase = new Chase(this,NavmeshAgent, _animator, _attackRange, _chaseSpeed);
            Death death = new Death(NavmeshAgent, _animator);
            Move move = new Move(NavmeshAgent, _animator);

            _stateMachine = new StateMachine();
            At(bomb, attack, () => bomb.BombIsAlive);
            _stateMachine.SetState(move);
            _stateMachine.AddAnyTransition(death, () => death.IsDeath);
            
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            At(move, chase, HasTarget());//player in range
            At(chase, attack, AttackRange());//remaining distance
            At(attack, chase, ExitAttackRange());//remaining distance
            At(chase, move, Targetnull());
            
            Func<bool> HasTarget() => () => Target != null;
            //Func<bool> HosNoTarget() => () => Target == null;
            Func<bool> AttackRange() => () => Target != null && chase.IsPlayerInRange;
            Func<bool> ExitAttackRange() => () => Target != null && !chase.IsPlayerInRange;
            Func<bool> Targetnull()=>()=> Target != null;
        }
        
       
        private void Update() => _stateMachine.Tick();

    } 
}
