using Datas.ValueObject;
using Enums;
using Keys;
using Managers;
using System;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {

        #region Self Variables

        #region Serialized Variables,

        [SerializeField] private PlayerManager playerManager;

        [SerializeField] private new Rigidbody rigidbody;
        #endregion

        #region Private Variables

        private PlayerMovementData _data;

        private GameObject _currentParent;

        private bool _isReadyToMove;

        private Vector2 _inputVector;

        private TurretStatus _turretStatus;

        #endregion

        #endregion
        private void Awake()
        {
            
            _turretStatus =new TurretStatus();

            SetCurrentParrent();
        }

        public void SetMovementData(PlayerMovementData movementData)
        {
             _data = movementData;
        }
        public void UpdateInputValues(HorizontalInputParams inputParams)
        {   
            _inputVector = inputParams.MovementVector;

            EnableMovement(_inputVector.sqrMagnitude > 0);

            PlayerMove();
        }
        private void EnableMovement(bool movementStatus)
        {
             _isReadyToMove = movementStatus;
        }

        private void PlayerMove()
        {

            if (_isReadyToMove)
            {
                if (_turretStatus == TurretStatus.Inplace) 
                {
                    rigidbody.velocity = Vector3.zero; 

                    transform.rotation = new Quaternion(0, 0, 0, 0); 

                    return; 
                }

                Move();
                Rotate();

            }
            else if (rigidbody.velocity != Vector3.zero)
            {
                rigidbody.velocity = Vector3.zero;
            }
        }
        private void Move()
        {
            var velocity = rigidbody.velocity;

            velocity = new Vector3(_inputVector.x, velocity.y, _inputVector.y) * _data.Speed;

            rigidbody.velocity = velocity;
        }
        private void Rotate()
        {
            Vector3 movementDirection = new Vector3(_inputVector.x, 0, _inputVector.y);

            if (movementDirection == Vector3.zero) return;

            Quaternion _toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, _toRotation, 30);

        }

        private void SetCurrentParrent()
        {
            _currentParent = transform.parent.gameObject;
        }
        public void EnterToTurret(GameObject turretObj)
        {
            Vector3 turretPos = turretObj.transform.position;

            transform.position = new Vector3(turretPos.x, transform.position.y, turretPos.z - 2f);

            transform.parent = turretObj.transform;

            _turretStatus = TurretStatus.Inplace;
        }

        public void ExitToTurret()//We exit as joystick İnput
        {
            if ((_data.ExitClampLeftSide < _inputVector.x && _data.ExitClampBackSide > _inputVector.y) && (_data.ExitClampRightSide > _inputVector.x && _data.ExitClampBackSide > _inputVector.y))
            {
                transform.parent = _currentParent.transform;

                _turretStatus = TurretStatus.OutPlace;
            }
        }
    }
}