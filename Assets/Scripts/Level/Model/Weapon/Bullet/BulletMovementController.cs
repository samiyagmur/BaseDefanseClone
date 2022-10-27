﻿using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class BulletMovementController : MonoBehaviour
    {

        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField]
        private new Rigidbody rigidbody;
        private Transform _playerTransform;
        private const float fireDelay = 0.05f;
        private const float fireSpeed = 70;

        #endregion

        #region Private Variables

        #endregion

        #endregion
        private void OnEnable()
        {
            DOVirtual.DelayedCall(fireDelay, () => FireBullet());
        }

        private void FireBullet()
        {   
            rigidbody.AddRelativeForce(Vector3.forward * fireSpeed, ForceMode.VelocityChange);
        }
        private void OnDisable()
        {
            rigidbody.velocity = Vector3.zero;
        }

        internal void SetPlayerTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }
    }
}