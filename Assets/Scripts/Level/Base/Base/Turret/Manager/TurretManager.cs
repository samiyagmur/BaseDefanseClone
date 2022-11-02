using Controllers;
using Enums;
using Signals;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;

namespace Managers
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region SerializeField Variables

        [SerializeField]
        private List<TurretOtoAtackController> allOtoAtackTurrets;

        [SerializeField]
        private List<TurretShootController> allShootControllers;

        [SerializeField]
        private List<FireToTurret> allReadyToFire;

        [SerializeField]
        private List<GameObject> bots;

        [SerializeField]
        private List<SphereCollider> canOpenForTrigger = new List<SphereCollider>();

        #endregion SerializeField Variables

        #region Private Variables


        #endregion Private Variables
        private int _count;
        private int _currentStackCount;
        #endregion Self Variables

        #region Event Subscription

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            TurretSignals.Instance.onGenarateBot += OnGenarateBot;
            TurretSignals.Instance.onDieEnemy += OnDeadEnemy;

        }

        private void UnsubscribeEvents()
        {
            TurretSignals.Instance.onGenarateBot -= OnGenarateBot;
            TurretSignals.Instance.onDieEnemy -= OnDeadEnemy;
        }


        private void Start()
        {
            foreach (var item in canOpenForTrigger)
            {
                item.radius = 0;
            }
        }

        private async void OnGenarateBot(BotCreatType type)
        {
            bots[(int)type].gameObject.SetActive(true);
            _count= 0;
            while (_count < 15)
            {
               
                await Task.Delay(100);
                canOpenForTrigger[(int)type].radius = _count;

                _count++;
            }
        }
        private void OnDisable() => UnsubscribeEvents();

        #endregion Event Subscription

        #region BotController

      
       
        public void Attack(TurretId turretKey)
        {

            allReadyToFire[(int)turretKey].LoadMagazine(turretKey, allOtoAtackTurrets[(int)turretKey].GetTargetStatus(), 
                canOpenForTrigger[(int)turretKey]);
        }

        internal bool GetToStackStatus(TurretId turretKey)
        {
            _currentStackCount = AmmoManagerSignals.Instance.onGetCurrentTurretStackCount.Invoke(turretKey);

            return _currentStackCount > 0;
        }

        internal GameObject GetToRocked(TurretId turretKey)
        {
            return AmmoManagerSignals.Instance.onGetAmmoForFire?.Invoke(turretKey);
        }

        public void OtoRotate(TurretId turretKey)
        {
            allOtoAtackTurrets[(int)turretKey].RotateTurret();

            allOtoAtackTurrets[(int)turretKey].FollowToEnemy();
        }

        public void IsEnemyEnterTurretRange(GameObject enemy, TurretId turretKey)
        {
            allOtoAtackTurrets[(int)turretKey].AddDeathList(enemy);
        }

        public void SpinGattaling(TurretId turretKey)
        {
            allShootControllers[(int)turretKey].ActiveGattaling();
        }
        private  void OnDeadEnemy(TurretId turretKey,GameObject gameObject)
        {
            allOtoAtackTurrets[(int)turretKey].RemoveDeathList(gameObject);
        }

        public void IsEnemyExitTurretRange(TurretId turretKey, GameObject gameObject)
        {   
            allOtoAtackTurrets[(int)turretKey].RemoveDeathList(gameObject);
        }

        #endregion BotController
    }
}