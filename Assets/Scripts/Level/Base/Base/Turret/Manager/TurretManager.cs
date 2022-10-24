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
        private List<FireToTurret> _allReadyToFire;

        [SerializeField]
        private List<GameObject> bots;

        [SerializeField]
        private List<SphereCollider> canOpenForTrigger = new List<SphereCollider>();

        #endregion SerializeField Variables

        #region Private Variables


        #endregion Private Variables

        #endregion Self Variables

        #region Event Subscription

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            TurretSignals.Instance.onGenarateBot += OnGenarateBot;
            TurretSignals.Instance.onDieEnemy += OnDeadEnemy;
           // TurretSignals.Instance.onReloadStack += OnReloadStack;
        }

        private void UnsubscribeEvents()
        {
            TurretSignals.Instance.onGenarateBot -= OnGenarateBot;
            TurretSignals.Instance.onDieEnemy -= OnDeadEnemy;
           // TurretSignals.Instance.onReloadStack -= OnReloadStack;
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

        

            count= 0;
            while (count < 15)
            {
                Debug.Log(count);
                await Task.Delay(100);
                canOpenForTrigger[(int)type].radius = count;

                

                count++;
            }
        }
        private void OnDisable() => UnsubscribeEvents();

        #endregion Event Subscription



        #region BotController

        private int currentStackCount;
        private int count;

        private int triggerCount;
        private int ss;

        public void Attack(TurretKey turretKey)
        {
            _allReadyToFire[(int)turretKey].LoadMagazine(turretKey, allOtoAtackTurrets[(int)turretKey].GetTargetStatus(), 
                canOpenForTrigger[(int)turretKey]);
        }

        internal bool GetToStackInfo(TurretKey turretKey)
        {
            currentStackCount = AmmoManagerSignals.Instance.onGetCurrentTurretStackCount.Invoke(turretKey);

            return currentStackCount > 0;
        }

        internal GameObject GetToRocked(TurretKey turretKey)
        {
            return AmmoManagerSignals.Instance.onGetAmmoForFire?.Invoke(turretKey);
        }

        public void OtoRotate(TurretKey turretKey)
        {
            allOtoAtackTurrets[(int)turretKey].RotateTurret();

            allOtoAtackTurrets[(int)turretKey].FollowToEnemy();
        }

        public void IsEnemyEnterTurretRange(GameObject enemy, TurretKey turretKey)
        {
            allOtoAtackTurrets[(int)turretKey].AddDeathList(enemy);
        }

        public void SpinGattaling(TurretKey turretKey)
        {
            allShootControllers[(int)turretKey].ActiveGattaling();
        }

        //private void OnReloadStack(TurretKey turretKey)
        //{
        //    Attack(turretKey);

        //}

        private  void OnDeadEnemy(TurretKey turretKey)
        {

            allOtoAtackTurrets[(int)turretKey].RemoveDeathList();





            //Attack(turretKey);
        }

        public void IsEnemyExitTurretRange(TurretKey turretKey)
        {
            allOtoAtackTurrets[(int)turretKey].RemoveDeathList();
        }

        #endregion BotController
    }
}