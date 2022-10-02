using Keys;
using UnityEngine;
using System;
using Enums;
using System.Collections.Generic;
using DG.Tweening;
using Datas.ValueObject;

namespace Controllers
{
  
    public class TurretMovementController : MonoBehaviour
    {
        private float _horizontalInput;
        private float _verticalInput;
        private TurretStatus _status;
        private TurretMovementData _movementDatas;



        public void SetMovementDatas(TurretMovementData movementDatas) => _movementDatas = movementDatas;

        public void SetInputParams(HorizontalInputParams input)//it can be turn on interface
        {
            _horizontalInput = input.MovementVector.x;
            _verticalInput = input.MovementVector.y;
            EnterToTaret();
        }

        private void EnterToTaret()//It can be abstract
        {
            if (_status == TurretStatus.Inplace) 
            {
                if ((-0.9f < _horizontalInput && _verticalInput > 0.3f) && (+0.9f > _horizontalInput && _verticalInput > 0.3f))
                {
                   Rotate();//
                }
            }
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }       
        }
        private void Rotate()
        {
            Vector3 _movementDirection = new Vector3(_horizontalInput, 0, _verticalInput);
            if (_movementDirection == Vector3.zero) return;
            Quaternion _toRotation = Quaternion.LookRotation(_movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _toRotation, _movementDatas.TurretTurnSpeed);
            //Debug.DrawRay(transform.position, transform.forward*15f, Color.red, 0.1f);
        }
       
        public void ActiveTurretWithPlayer()
        {
            _status = TurretStatus.Inplace;

        }
        public void DeactiveTurretWithPlayer()
        {
            _status = TurretStatus.OutPlace;
        }
    }
}