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
        private TurretOtoAtackController _chosenAtackTurret;

        private TurretShootController _chosenShootController;

        private FireToTurret _readyToAtack;

        private TurretOtoAtackController _chosenTurretList;
        private TurretOtoAtackController _chosenRemoveTurretList;
        private TurretMovementController _chosenMovementController;
        private List<TurretMovementController> _allTurretMovementController;

        TurretStatus turretStatus;

        #endregion

        #endregion

        #region Get&SetData

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
        private float _timer = 0.5f;

        internal void IsSelectCurrentTurret(GameObject gameObject)
        {
            _chosenAtackTurret = gameObject.GetComponentInChildren<TurretOtoAtackController>();

            _chosenShootController = gameObject.GetComponentInChildren<TurretShootController>();

            _readyToAtack = gameObject.GetComponentInChildren<FireToTurret>();
        }

        private void FixedUpdate() => IsAttackToEnemy();

        internal  void IsAttackToEnemy()
        {
            if (_chosenAtackTurret!=null|| turretStatus == TurretStatus.Inplace)
            {
                if (_chosenAtackTurret.GetTargetList().Count!=0|| turretStatus == TurretStatus.Inplace)
                {
                    _chosenShootController.ActiveGattaling();

                    _timer -= Time.deltaTime;

                    if (_timer < 0)
                    {
                        _timer = 0.5f;

                        _readyToAtack.FireToRocked();
                    }
                    if (turretStatus == TurretStatus.Inplace) return;

                    OtoRotate();

                }
            }
        }

        private void OtoRotate()
        {
            _chosenAtackTurret.RotateTurret();

            _chosenAtackTurret.FollowToEnemy();
        }

        public  void IsEnemyEnterTurretRange(GameObject enemy, GameObject gameObject)
        {
            _chosenTurretList = gameObject.GetComponentInChildren<TurretOtoAtackController>();

            _chosenTurretList.AddDeathList(enemy);
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

            IsSelectCurrentTurret(gameObject);

            turretStatus = TurretStatus.Inplace;
        }

        public void IsExitUser(GameObject gameObject)
        {
            _chosenMovementController = gameObject.GetComponentInChildren<TurretMovementController>();

            _chosenMovementController.TurretActivationWithPlayer(TurretStatus.OutPlace);

            turretStatus = TurretStatus.OutPlace;
        }

        #endregion



        
    }
}