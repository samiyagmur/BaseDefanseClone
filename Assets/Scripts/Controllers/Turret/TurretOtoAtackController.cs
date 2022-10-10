﻿using Datas.ValueObject;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Controllers
{
    public class TurretOtoAtackController : MonoBehaviour
    {
      
        #region Self Variabels
        #region Private Variables
        private Queue<GameObject> _deadList = new Queue<GameObject>();
        private GameObject botTarget;
        private TurretOtoAtackData _turretOtoAtackDatas;

        private Tweener _tween;
        private TurretOtoAtackController _chosenAtackTurret;
        #endregion
        #endregion
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        public void SetOtoAtackDatas(TurretOtoAtackData turretOtoAtackDatas) => _turretOtoAtackDatas = turretOtoAtackDatas;
        public void AddDeathList(GameObject enemy)
        {
            _deadList.Enqueue(enemy);

            botTarget = _deadList.Peek();
        }


        public void RemoveDeathList()
        {
            if (_deadList.Count != 0)
            {
                _deadList.Dequeue();
                botTarget = _deadList.Peek();
            }
        }

        public void FollowToEnemy()
        {
            if (_deadList.Count == 0) return;

            ArangeRotateRotation(botTarget.transform);
        }

        private void ArangeRotateRotation(Transform _movementDirection)
        {
            if (_movementDirection.position == Vector3.zero) return;

            Vector3 horizontalRotation = new Vector3(_movementDirection.position.x, 0, _movementDirection.position.z);

            Vector3 _relativePos = horizontalRotation - transform.position;

            Quaternion _toRotation = Quaternion.LookRotation(_relativePos);

            _tween.ChangeEndValue(_toRotation, true);
        }

        public void RotateTurret()
        {
            if (_deadList.Count == 0) return;
            _tween = transform.DORotateQuaternion(botTarget.transform.rotation, 0.499f).SetAutoKill(true);
        }
        public Queue<GameObject> GetTargetList()
        {
            return _deadList;
        }
    }
}