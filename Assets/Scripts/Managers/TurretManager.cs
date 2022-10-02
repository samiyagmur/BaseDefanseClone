using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Interfaces;
using Keys;
using Signals;
using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public class TurretManager : MonoBehaviour
    {

        #region Self Variables
        #region SerializeField Variables
        [SerializeField]
        private TurretMovementController _movementController;
        [SerializeField]
        private TurretOtoAtackController _otoAtackController;
        [SerializeField]
        private TurretShootController ShootController;
        #endregion

        #region Private Variables
        private TurretData turretData;
        #endregion

        #endregion

        #region Get&SetData
        private void Awake() => Init();

        private void Init()
        {
            turretData = GetTurretData();
            SetMovementData();
            OtoAtackData();
            GattalingRotateData();
        }

        private TurretData GetTurretData() => Resources.Load<CD_TurretData>("Data/CD_TurretData").turretDatas;

        private void SetMovementData() => _movementController.SetMovementDatas(turretData.MovementDatas);

        private void OtoAtackData() => _otoAtackController.SetOtoAtackDatas(turretData.TurretOtoAtackDatas);

        private void GattalingRotateData() => ShootController.SetGattalingRotateDatas(turretData.gattalingRotateDatas);

        public GameObject GetGameObject() => gameObject;
        #endregion

        #region Event Subscription
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnGetInputValues;
            TurretSignals.Instance.onPressTurretButton += OnPressTurretButton;
            TurretSignals.Instance.onDeadEnemy += OnDeadEnemy;//İnterfacele gelcek
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
            TurretSignals.Instance.onPressTurretButton -= OnPressTurretButton;
            TurretSignals.Instance.onDeadEnemy -= OnDeadEnemy;

        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion

        #region SubsciribeMethods
        public void OnPressTurretButton()
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Collider>().enabled = false;
        }

        private void OnDeadEnemy() => IsEnemyExitTurretRange();

        #endregion

        #region BotController
        public void IsFollowEnemyInTurretRange()
        {
            ShootController.ActiveGattaling();
            //transform.GetComponentInChildren<AmmoContaynerManager>().IsTurretAttack();
            _otoAtackController.FollowToEnemy();
        }

        public void IsEnemyEnterTurretRange(GameObject enemy) => _otoAtackController.AddDeathList(enemy);
        public void IsEnemyExitTurretRange()
        {
            _otoAtackController.RemoveDeathList();
            ShootController.DeactiveGattaling();
        } 
        #endregion

        #region PlayerController
        private void OnGetInputValues(HorizontalInputParams value) => _movementController.SetInputParams(value);

        public void IsEnterUser() => _movementController.ActiveTurretWithPlayer();

        public void IsExitUser() => _movementController.DeactiveTurretWithPlayer();     
        #endregion
       

    }
}