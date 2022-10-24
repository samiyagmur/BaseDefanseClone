//using Keys;
//using UnityEngine;
//using System;
//using Enums;
//using System.Collections.Generic;
//using DG.Tweening;
//using Datas.ValueObject;
//using Managers;

//namespace Controllers
//{
  
//    public class TurretMovementController : MonoBehaviour
//    {
//        private float _horizontalInput;
//        private float _verticalInput;
//        private TurretStatus _status;
//        private float _moveSpeed=30f;

//        [SerializeField]
//        private TurretManager _turretManager;

//        public void SetInputParams(HorizontalInputParams input)//it can be turn on interface
//        {

            
//            //if (_status == TurretStatus.OutPlace) return;

//            _horizontalInput = input.MovementVector.x;
//            _verticalInput = input.MovementVector.y;
//            EnterToTaret();
//        }

//        private void EnterToTaret()//It can be abstract
//        {
            
//                if ((-0.9f < _horizontalInput && _verticalInput > 0.3f) && (+0.9f > _horizontalInput && _verticalInput > 0.3f))
//                {
//                   Rotate();//
//                }

//                transform.rotation = new Quaternion(0, 0, 0, 0);
                   
//        }
//        private void Rotate()
//        {
//            Vector3 _movementDirection = new Vector3(_horizontalInput, 0, _verticalInput);
//            if (_movementDirection == Vector3.zero) return;
//            Quaternion _toRotation = Quaternion.LookRotation(_movementDirection, Vector3.up);
//            transform.rotation = Quaternion.RotateTowards(transform.rotation, _toRotation, _moveSpeed);

//        }
        
//        public void TurretActivationWithPlayer(TurretStatus status)
//        {   
//            Debug.Log(_status);

//            _status = status;

//        }

//    }
//}