using Controllers;
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


        private IUsuable movementController;

        private IPlayerHitAble playerHitAble;
        #endregion
        #endregion
        private void Awake()
        {
            movementController = new TurretMovementController();

        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnGetInputValues;

        }
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnGetInputValues;

        }
        private void OnDisable() => UnsubscribeEvents();

        private void OnGetInputValues(HorizontalInputParams value)
        {
            movementController.SetInputParams(value);
        }


        

    }
}