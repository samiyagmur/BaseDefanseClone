using AI.States;
using Data.ValueObject;
using Enums;
using Interfaces;
using Signals;
using Sirenix.OdinInspector;
using StateBehavior;
using StateMachines.AIBrain.Workers.MoneyStates;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrain.MoneyWorkers
{
    public class MoneyWorkerAIBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [BoxGroup("Public Variables")]
        public Transform CurrentTarget;

        #endregion Public Variables

        #region Serilizable Variables

        [BoxGroup("Serializable Variables")]
        [SerializeField]
        private WorkerType workerType;

        #endregion Serilizable Variables

        #region Private Variables

        [ShowInInspector]
        private WorkerAITypeData _workerTypeData;

        private Animator _animator;
        private NavMeshAgent _navmeshAgent;

        #region States

        private Transform _moneytransform;
        private MoveToGateState _moveToGateState;
        private SearchState _searchState;
        private StackMoneyState _stackMoneyState;
        private DropMoneyOnGateState _dropMoneyOnGateState;
        private StateMachine _stateMachine;

        [ShowInInspector]
        private Vector3 waitPos;

        #endregion States

        #region Worker Game Variables

        [ShowInInspector]
        private int _currentStock = 0;

        private const float _delay = 0.2f;

        #endregion Worker Game Variables

        #endregion Private Variables

        #endregion Self Variables

        private void Awake()
        {
            _workerTypeData = GetWorkerType();
            SetWorkerComponentVariables();
        }

        private void Start()
        {
            GetReferenceStates();
        }

        #region Data Jobs

        private WorkerAITypeData GetWorkerType()
        {
            return MoneyWorkerSignals.Instance.onGetMoneyAIData?.Invoke(workerType);
        }

        private void SetWorkerComponentVariables()
        {
            _navmeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
        }

        #endregion Data Jobs

        #region Worker State Jobs

        private void GetReferenceStates()
        {
            _searchState = new SearchState(_navmeshAgent, _animator, this);
            _moveToGateState = new MoveToGateState(_navmeshAgent, _animator, waitPos, _workerTypeData.MaxSpeed);
            _stackMoneyState = new StackMoneyState(_navmeshAgent, _animator, this, _workerTypeData.MaxSpeed);
            _dropMoneyOnGateState = new DropMoneyOnGateState(_navmeshAgent, _animator, waitPos);

            _stateMachine = new StateMachine();

            At(_moveToGateState, _searchState, HasArrive());
            At(_searchState, _stackMoneyState, HasCurrentTargetMoney());
            At(_stackMoneyState, _searchState, _stackMoneyState.IsArriveToMoney());
            At(_stackMoneyState, _dropMoneyOnGateState, HasCapacityFull());
            At(_dropMoneyOnGateState, _searchState, HasCapacityNotFull());

            _stateMachine.SetState(_moveToGateState);
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> HasArrive() => () => _moveToGateState.IsArrive;
            Func<bool> HasCurrentTargetMoney() => () => CurrentTarget != null;
            Func<bool> HasCapacityFull() => () => !IsAvailable();
            Func<bool> HasCapacityNotFull() => () => IsAvailable();
        }

        private void Update() => _stateMachine.Tick();

        #endregion Worker State Jobs

        #region General Jobs

        public bool IsAvailable() => _currentStock < _workerTypeData.CapacityOrDamage;

        public void SetDest()
        {
            GetMoneyPosition();
            if (_moneytransform == null) return;

            CurrentTarget = _moneytransform;

            if (CurrentTarget != null)
                _navmeshAgent.SetDestination(CurrentTarget.position);
        }

        public void SetInitPosition(Vector3 slotPosition)
        {
            waitPos = slotPosition;
        }

        public Transform GetMoneyPosition()
        {
            _moneytransform = MoneyWorkerSignals.Instance.onGetTransformMoney?.Invoke(this.transform);

            return _moneytransform;
        }

        private IEnumerator SearchTarget()
        {
            while (CurrentTarget == null)
            {
                SetDest();

                yield return new WaitForSeconds(_delay);
            }
        }

        public void StartSearch(bool isStartedSearch)
        {
            if (isStartedSearch)
            {
                StartCoroutine(SearchTarget());
            }
            else
            {
                StopCoroutine(SearchTarget());
            }
        }

        public void SetCurrentStock()
        {
            if (_currentStock < _workerTypeData.CapacityOrDamage)
                _currentStock++;
        }

        public void RemoveAllStock()
        {
            for (int i = 0; i < _workerTypeData.CapacityOrDamage; i++)
            {
                if (_currentStock > 0)
                    _currentStock--;
                else
                    _currentStock = 0;
            }

            //remove all stack
        }

        #endregion General Jobs
    }
}