using Abstraction;
using AIBrain.Enemy.State;

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
        private StateMachine _stateMachine;
        private Animator _animator;
        private NavMeshAgent _navmeshAgent;

        public Transform Target;

        private void Awake()
        {
            GetReferanceState();
        }
        private void GetReferanceState()
        {
            _navmeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            Bomb bomb= new Bomb(_navmeshAgent, _animator);
            Attack attack= new Attack(_navmeshAgent, _animator);
            Chase chase = new Chase(_navmeshAgent, _animator);
            Death death = new Death(_navmeshAgent, _animator);
            Move move = new Move(_navmeshAgent, _animator);

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
            Func<bool> AttackRange() => () => Target != null && chase.IsPlayerInRange;
            Func<bool> ExitAttackRange() => () => Target != null && !chase.IsPlayerInRange;
            Func<bool> Targetnull()=>()=> Target != null;
        }
        
       
        private void Update() => _stateMachine.Tick();

    } 
}
