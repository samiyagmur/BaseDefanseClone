using AIBrain.AmmoWorkers;
using Controllers;
using Data.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class AmmoManager : MonoBehaviour
    {
        #region Self-Private Variabels

        [SerializeField]
        private CD_AIData _cD_AIData;

        private int _delayStack = 125;

        [SerializeField]
        private List<AmmoDropZoneController> loadTurretStackController = new List<AmmoDropZoneController>();

        [SerializeField]
        private List<AmmoDropZoneController> selectTargetList = new List<AmmoDropZoneController>();

        [SerializeField]
        private List<AmmoDropZoneController> currentTurretStackController = new List<AmmoDropZoneController>();

        [ShowInInspector]
        private Queue<AmmoWorkerBrain> currentAmmoWorkersInTurretStackArae = new Queue<AmmoWorkerBrain>();

        #endregion Self-Private Variabels

        private int _counter;
        private AmmoWorkerAIData _ammoWorkerAIData;
        private AmmoDropZoneGridController _turretStackGridController;
        private int _maxStackCapasity;
        private AmmoDropZoneController _selectedTarget;
        private int _currentDelay;

        internal void Awake() => Init();

        private void Init()
        {
            _ammoWorkerAIData = _cD_AIData.AmmoWorkerAIDatas;
            _turretStackGridController = new AmmoDropZoneGridController();
            _maxStackCapasity = _ammoWorkerAIData.MaxStackCount;
        }

        private void Start()
        {
            _turretStackGridController.GanarateGrid();

            foreach (var item in loadTurretStackController)
            {
                item.LoadGrid(_turretStackGridController.LastPosition());

                if (item.gameObject.activeInHierarchy)
                {
                    currentTurretStackController.Add(item);
                }
            }
        }

        //private TurretKey onActiveTurretStack()
        //{
        //}

        #region Event Subscription

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea += OnPlayerEnterAmmoWorkerCreaterArea;
            AmmoManagerSignals.Instance.onSetAmmoStackStatus += OnSetAmmoStackStatus;
            AmmoManagerSignals.Instance.onGetAmmoForFire += OnGetAmmoToFire;
            AmmoManagerSignals.Instance.onGetCurrentTurretStackCount += OnGetCurrentTurretStackCount;
            AmmoManagerSignals.Instance.onIncreaseAmmoWorkerCapasity += OnIncreaseAmmoWorkerCapasity;
            AmmoManagerSignals.Instance.onIncreaseAmmoWorkerSpeed += onIncreaseAmmoWorkerSpeed;
            // AmmoManagerSignals.Instance.onActiveTurretStack += onActiveTurretStack;
        }

        private void UnsubscribeEvents()
        {
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea -= OnPlayerEnterAmmoWorkerCreaterArea;
            AmmoManagerSignals.Instance.onSetAmmoStackStatus -= OnSetAmmoStackStatus;
            AmmoManagerSignals.Instance.onGetAmmoForFire -= OnGetAmmoToFire;
            AmmoManagerSignals.Instance.onGetCurrentTurretStackCount -= OnGetCurrentTurretStackCount;
            AmmoManagerSignals.Instance.onIncreaseAmmoWorkerCapasity -= OnIncreaseAmmoWorkerCapasity;
            AmmoManagerSignals.Instance.onIncreaseAmmoWorkerSpeed -= onIncreaseAmmoWorkerSpeed;
            // AmmoManagerSignals.Instance.onActiveTurretStack -= onActiveTurretStack;
        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion Event Subscription

        private GameObject OnGetAmmoToFire(TurretId key)
        {
            var s = loadTurretStackController[(int)key].RemoveToStack();

            loadTurretStackController[(int)key].UpDateList();

            return s;
        }

        private int OnGetCurrentTurretStackCount(TurretId key)
        {
            return loadTurretStackController[(int)key].GetCurrentCount();
        }

        internal void WhenGetTurretStackInfo(AmmoWorkerBrain ammoWorkerBrain)
        {
            foreach (var item in loadTurretStackController)
            {
                if (item.gameObject.activeInHierarchy)
                {
                    currentTurretStackController.Add(item);
                }
            }

            if (selectTargetList.Count == 0)
            {
                selectTargetList = currentTurretStackController.OrderBy(x => x.GetCurrentCount()).ToList();
            }

            _selectedTarget = selectTargetList[0];

            currentTurretStackController.Clear();

            currentTurretStackController.TrimExcess();

            selectTargetList.RemoveAt(0);

            selectTargetList.TrimExcess();

            ammoWorkerBrain.SetTargetTurretStack(_selectedTarget.gameObject);
        }

        internal void WhenEnterTurretStack(AmmoDropZoneController turretStackController, AmmoWorkerStackController ammoWorkerStackController)
        {
            turretStackController.AddStack(ammoWorkerStackController.SendAmmoStack());
        }

        private void OnSetAmmoStackStatus(AmmoStackStatus status)
        {
            if (currentAmmoWorkersInTurretStackArae.Count != 0)
            {
                currentAmmoWorkersInTurretStackArae.Peek().ChangeAmmoWorkerStackStatus(status);

                //_currentAmmoWorkersInTurretStackArae.Dequeue();
            }
        }

        private void onIncreaseAmmoWorkerSpeed(float amount)
        {
            foreach (var item in currentAmmoWorkersInTurretStackArae)
            {
                item.IncreaseSpeed(amount);
            }
        }

        private void OnIncreaseAmmoWorkerCapasity(int amount)
        {
            _maxStackCapasity += amount;

            _currentDelay = _delayStack * _maxStackCapasity - (_maxStackCapasity - 1);
        }

        internal void WhenEnterTurretStack(AmmoWorkerBrain ammoWorkerBrain, TurretId turretKey)
        {
            TurretSignals.Instance.onReloadStack?.Invoke(turretKey);
            ammoWorkerBrain.IsLoadTurret(true);
        }

        internal void WhenExitTurretStack(AmmoWorkerBrain ammoWorkerBrain)
            => ammoWorkerBrain.IsLoadTurret(false);

        public void WhenExitOnTurretStack(AmmoWorkerStackController ammoWorkerStackController)
            => ammoWorkerStackController.SetClearWorkerStackList();//clear

        internal void WhenAmmoworkerEnterAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain)
            => ammoWorkerBrain.SetTriggerInfo(true);

        internal void WhenAmmoworkerExitAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain)
            => ammoWorkerBrain.SetTriggerInfo(false);

        internal async void WhenStayOnAmmoWareHouse(AmmoWorkerBrain ammoWorkerBrain, AmmoWorkerStackController ammoWorkerStackController)//clear
        {
            if (_counter < _ammoWorkerAIData.MaxStackCount)
            {
                ammoWorkerStackController.AddStack(_ammoWorkerAIData.AmmoWareHouse, ammoWorkerBrain.gameObject.transform,
                                          GetObject(PoolType.Ammo.ToString()), _maxStackCapasity);
                _counter++;
            }
            else
            {
                _counter = 0;
                await Task.Delay(_currentDelay);
                ammoWorkerBrain.ChangeAmmoWorkerStackStatus(AmmoStackStatus.Fill);
            }
        }

        public GameObject GetObject(string poolName) => ObjectPoolManager.Instance.GetObject<GameObject>(poolName);

        private void OnPlayerEnterAmmoWorkerCreaterArea(Transform workerCreater)
        {
            AddAmmaWorker(workerCreater);
        }

        public void AddAmmaWorker(Transform workerCreater)
        {
            GameObject ammoWorker = GetObject(PoolType.AmmoWorkerAI.ToString());

            ammoWorker.transform.position = workerCreater.position;
            AmmoWorkerBrain ammoWorkerBrain = ammoWorker.GetComponent<AmmoWorkerBrain>();
            currentAmmoWorkersInTurretStackArae.Enqueue(ammoWorkerBrain);
        }

        public void ResetItems() => _counter = 0;
    }
}