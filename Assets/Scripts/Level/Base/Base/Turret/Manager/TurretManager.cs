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

        [SerializeField]
        private List<TurretOtoAtackController> _allOtoAtackTurrets;

        [SerializeField]
        private List<TurretShootController> _allShootControllers;

        [SerializeField]
        private List<TurretMovementController> _allTurretMovementControllers;

        [SerializeField]
        private List<FireToTurret> _allReadyToFire;

        #endregion

        #region Private Variables

        private TurretOtoAtackController _chosenAtackTurret;

        private TurretShootController _chosenShootController;

        private FireToTurret _readyToAtack;

        private TurretMovementController _chosenMovementController;

        private int ammoCount = 0;

        private TurretKey currentTurretKey;

        private GameObject currentBullet;


        private TurretStatus turretStatus;
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

        internal void IsSelectCurrentTurret(TurretKey turretKey)
        {
            _chosenAtackTurret = _allOtoAtackTurrets[(int)turretKey];

            _chosenShootController = _allShootControllers[(int)turretKey];

            _readyToAtack = _allReadyToFire[(int)turretKey];


        }

        private void FixedUpdate() => IsAttackToEnemy();

        internal  void IsAttackToEnemy()
        {
          
            if (_chosenAtackTurret!=null)
            {
                if (_chosenAtackTurret.GetTargetList().Count!=0|| turretStatus == TurretStatus.Inplace)
                {
                    _chosenShootController.ActiveGattaling();

                    _timer -= Time.deltaTime;

                    if (_timer < 0)
                    {
                        _timer = 0.5f;

                        ammoCount++;
                        if (ammoCount==4)
                        {
                            currentBullet = AmmoManagerSignals.Instance.onGetAmmoToFire(currentTurretKey);

                            ammoCount=0;
                        }


                        _readyToAtack.FireToRocked(currentBullet);
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

        public  void IsEnemyEnterTurretRange(GameObject enemy)
        {

            _chosenAtackTurret.AddDeathList(enemy);
        }

        public void IsEnemyExitTurretRange()
        {
            _chosenAtackTurret.RemoveDeathList();

            _chosenShootController.DeactiveGattaling();
        }
        #endregion

        #region PlayerController
        private void OnGetInputValues(HorizontalInputParams value)
        {
            _chosenMovementController.SetInputParams(value);
        }

        public void IsEnterUser(TurretKey turretKey)
        {
            _chosenMovementController = _allTurretMovementControllers[(int)turretKey];

            _chosenMovementController.TurretActivationWithPlayer(TurretStatus.Inplace);
            
            IsSelectCurrentTurret(turretKey);

            turretStatus = TurretStatus.Inplace; 
        }

        public void IsExitUser()
        {
            _chosenMovementController.TurretActivationWithPlayer(TurretStatus.OutPlace);

            turretStatus = TurretStatus.OutPlace;
        }



        #endregion




    }
}