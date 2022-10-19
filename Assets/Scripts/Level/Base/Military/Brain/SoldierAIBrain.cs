using System;
using System.Collections.Generic;
using Abstraction;
using AI.States;
using Data.UnityObject;
using Data.ValueObject;
using Interfaces;
using StateBehavior;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.SoldierBrain
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SoldierAIBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public bool HasReachedSlotTarget;
        public bool HasReachedFrontYard;
        public bool HasEnemyTarget = false;

        public Transform TentPosition;
        public Transform FrontYardStartPosition;
        public List<IDamagable> enemyList = new List<IDamagable>();
        public Transform EnemyTarget;
        public IDamagable DamageableEnemy;
        public Transform WeaponHolder;
        public bool HasSoldiersActivated;
        #endregion

        #region Serialized Variables
        [SerializeField]
        private Animator animator;
        private int _health;
        public int Health { get => _health; set => _health = value; }
        #endregion

        #region Private Variables
        private NavMeshAgent _navMeshAgent;

        [Header("Data")]
        private SoldierAIData _data;

        private StateMachine _stateMachine;
        private Vector3 _slotTransform;

        #endregion
        #endregion
        private void Awake()
        {
            _data = GetSoldierAIData();
        }
        private void Start()
        {
            GetStateReferences();
        }
        private SoldierAIData GetSoldierAIData() => Resources.Load<CD_AIData>("Data/CD_AIData").SoldierAIDatas;
        private void GetStateReferences()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            Idle idle = new Idle(this,TentPosition, _navMeshAgent, animator);
            MoveToSlotZone moveToSlotZone = new MoveToSlotZone(this, _navMeshAgent, HasReachedSlotTarget, _slotTransform, animator);
            Wait wait = new Wait(animator, _navMeshAgent);
            MoveToFrontYard moveToFrontYard = new MoveToFrontYard(this, _navMeshAgent, FrontYardStartPosition, animator);
            Patrol patrol = new Patrol(this, _navMeshAgent, animator);
            SoldiersAtack attack = new SoldiersAtack(this, _navMeshAgent, animator);
            _stateMachine = new StateMachine();

            At(idle, moveToSlotZone, hasSlotTransformList());
            At(moveToSlotZone, moveToFrontYard, hasSoldiersActivated());
            At(moveToSlotZone, wait, hasReachToSlot());
            At(wait, moveToFrontYard, hasSoldiersActivated());
            At(moveToFrontYard, patrol, hasReachedFrontYard());
            At(patrol, attack, hasEnemyTarget());
            At(attack, patrol, hasNoEnemyTarget());

            _stateMachine.SetState(idle);
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> hasSlotTransformList() => () => _slotTransform != null;
            Func<bool> hasReachToSlot() => () => _slotTransform != null && HasReachedSlotTarget;
            Func<bool> hasSoldiersActivated() => () => FrontYardStartPosition != null && HasSoldiersActivated;
            Func<bool> hasReachedFrontYard() => () => FrontYardStartPosition != null && HasReachedFrontYard;
            Func<bool> hasEnemyTarget() => () => HasEnemyTarget;
            Func<bool> hasNoEnemyTarget() => () => !HasEnemyTarget;
        }
        private void Update() => _stateMachine.Tick();
        public void GetSlotTransform(Vector3 slotTransfrom) => _slotTransform = slotTransfrom;
    }
}