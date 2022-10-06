using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Interfaces;
using Keys;
using Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class TurretManager : MonoBehaviour
    {


        #region Self Variables
        #region SerializeField Variables

        #endregion

        #region Private Variables
        private TurretData turretData;

        private GameObject _rocked;
        private TurretOtoAtackController _chosenAtackTurret;
        private TurretShootController _chosenShootController;

        private FireToTurret _chosenReadyTurret;

        private int rockedDamage = 50;
        private TurretOtoAtackController _chosenAddTurretList;
        private TurretOtoAtackController _chosenRemoveTurretList;
        private TurretMovementController _chosenMovementController;
        private List<TurretMovementController> _allTurretMovementController;
        private List<TurretOtoAtackController> _allTurretOtoAtackController;
        private List<TurretShootController> _allTurretShootController;
        #endregion

        #endregion

        #region Get&SetData
        private void Awake() => Init();

        private void Init()
        {
            turretData = GetTurretData();
            SetAllCompanentData();

        }

        private TurretData GetTurretData() => Resources.Load<CD_TurretData>("Data/CD_TurretData").turretDatas;

        private void SetAllCompanentData() 
        {
            _allTurretMovementController = GetComponentsInChildren<TurretMovementController>().ToList();
            foreach (var item in _allTurretMovementController) 
                         item.SetMovementDatas(turretData.MovementDatas);

            _allTurretOtoAtackController = GetComponentsInChildren<TurretOtoAtackController>().ToList();
            foreach (var item in _allTurretOtoAtackController)
                         item.SetOtoAtackDatas(turretData.TurretOtoAtackDatas);

            _allTurretShootController = GetComponentsInChildren<TurretShootController>().ToList();
            foreach (var item in _allTurretShootController)
                         item.SetGattalingRotateDatas(turretData.gattalingRotateDatas);
        }



        #endregion

        #region Event Subscription
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnGetInputValues;
            TurretSignals.Instance.onPressTurretButton += OnPressTurretButton;
           // TurretSignals.Instance.onDieEnemy += OnDeadEnemy;
         
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
            TurretSignals.Instance.onPressTurretButton -= OnPressTurretButton;
          //  TurretSignals.Instance.onDieEnemy -= OnDeadEnemy;


        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion

        #region SubsciribeMethods
        public void OnPressTurretButton()
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Collider>().enabled = false;
        }



       // public void OnDeadEnemy() => IsEnemyExitTurretRange();

        #endregion

        #region BotController
        public void IsFollowEnemyInTurretRange(GameObject gameObject)
        {

            _chosenAtackTurret = gameObject.GetComponentInChildren<TurretOtoAtackController>();

            _chosenShootController = gameObject.GetComponentInChildren<TurretShootController>();

            _chosenAtackTurret.FollowToEnemy();

            _chosenShootController.ActiveGattaling();
        }

        internal void IsAttackToEnemy()
        {
            _chosenReadyTurret = GetComponentInChildren<FireToTurret>();

            _chosenReadyTurret.FireToRocked();

        }

        public void IsEnemyEnterTurretRange(GameObject enemy, GameObject gameObject)
        {
            _chosenAddTurretList = gameObject.GetComponentInChildren<TurretOtoAtackController>();

            _chosenAddTurretList.AddDeathList(enemy);
        }

        public void IsEnemyExitTurretRange(GameObject gameObject)
        {
            _chosenRemoveTurretList = gameObject.GetComponentInChildren<TurretOtoAtackController>();

            _chosenShootController = gameObject.GetComponentInChildren<TurretShootController>();

            _chosenRemoveTurretList.RemoveDeathList();

            _chosenShootController.DeactiveGattaling();
        }
        #endregion

        #region PlayerController
        private void OnGetInputValues(HorizontalInputParams value)
        {
            _allTurretMovementController = GetComponentsInChildren<TurretMovementController>().ToList();

            foreach (var item in _allTurretMovementController)
                         item.SetInputParams(value);    
        }

        public void IsEnterUser(GameObject gameObject)
        {
            _chosenMovementController = gameObject.GetComponentInChildren<TurretMovementController>();

            _chosenMovementController.TurretActivationWithPlayer(TurretStatus.Inplace);
        }

        public void IsExitUser(GameObject gameObject)
        {
            _chosenMovementController = gameObject.GetComponentInChildren<TurretMovementController>();

            _chosenMovementController.TurretActivationWithPlayer(TurretStatus.OutPlace);
        }

        #endregion



        //public void ReleaseObject(GameObject rocked, PoolType poolName)
        //{
        //    PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName,rocked);
        //}

        
    }
}