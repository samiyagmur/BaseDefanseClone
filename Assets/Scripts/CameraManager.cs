using Enums;
using Signals;
using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {   
        private Animator _animator;


        #region Event Subscription
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
        }
        private void OnDisable() => UnsubscribeEvents();

        #endregion
        private void ChangeCamera(CameraTypes type)
        {
            _animator.Play(type.ToString());
        }

        private void OnSetCameraTarget()
        {
           
        }

        private void OnReset()
        {
            ChangeCamera(CameraTypes.Level);
            OnSetCameraTarget();
        }

    }
}