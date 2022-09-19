using Controllers;
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

        #endregion

        #region Private Variables


        #endregion

        #endregion
        private void Awake()
        {
           
            //get data
        }

        

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnGetInputValues;
            TurretSignals.Instance.onPressTurretButton += OnPressTurretButton;
        }

        internal void IsHitEnemy(GameObject enemy)
        {
            _movementController.AddDeathList(enemy);
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
            TurretSignals.Instance.onPressTurretButton -= OnPressTurretButton;

        }
        private void OnDisable() => UnsubscribeEvents();
        public void OnPressTurretButton()
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Collider>().enabled = false;


            _movementController.ActiveTurretWithBot();

        }
        private void OnGetInputValues(HorizontalInputParams value)
        {   
            _movementController.SetInputParams(value);
        }
        public GameObject GetGameObject()
        {
            return gameObject;
        }
        public void IsEnterUser()
        {
            _movementController.ActiveTurretWithPlayer();
            
        }
        public void IsExitUser()
        {
            _movementController.DeactiveTurretWithPlayer();
        }
    }
}