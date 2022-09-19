using Keys;
using UnityEngine;
using System;
using Enums;
using System.Collections.Generic;

namespace Controllers
{
  
    public class TurretMovementController : MonoBehaviour
    {
        private float _horizontalInput;
        private float _verticalInput;
        private float _turretTurnSpeed;
        private TurretStatus _status;
        private TurretUserType _userStatus;
        private Vector3 _movementDirection;
        private Transform botTarget;
        private List<GameObject> _deathList = new List<GameObject>();
        private void Awake()
        {
            _userStatus = TurretUserType.Bot;
        }
        public void AddDeathList(GameObject enemy)
        {
            _deathList.Add(enemy);
            botTarget = _deathList[0].transform;
            Debug.Log(_deathList[0].transform.position);
            
            
        }
        public void SetInputParams(HorizontalInputParams input)//it can be turn on interface
        {
            _horizontalInput = input.MovementVector.x;
            _verticalInput = input.MovementVector.y;
        }
        private void FixedUpdate()
        {
            Rotate();
        }
        private void EnterToTaret()
        {
            if (_status == TurretStatus.Inplace) 
            {
                if ((-0.9f < _horizontalInput && _verticalInput > 0.3f) && (+0.9f > _horizontalInput && _verticalInput > 0.3f))
                {
                   // Rotate();//düzgün dönmüytor

                }
            }
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }       
        }
        
        private void Rotate()
        {
           
            //_movementDirection = _userStatus== TurretUserType.Player ?
            //        new Vector3(_horizontalInput, 0, _verticalInput) : 
                    _movementDirection = botTarget.localPosition; 

            if (_movementDirection == Vector3.zero) return;
            Quaternion _toRotation = Quaternion.LookRotation(_movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _toRotation,10);
            //Debug.DrawRay(transform.position, transform.forward*15f, Color.red, 0.1f);
        }
        
        public void ActiveTurretWithBot()
        {

            _userStatus = TurretUserType.Bot;
            _status = TurretStatus.Inplace;
        }

        public void ActiveTurretWithPlayer()
        {
            //_userStatus=TurretUserType.Player;
            _status = TurretStatus.Inplace;
        }
        internal void DeactiveTurretWithPlayer()
        {
            //_userStatus = TurretUserType.Player;
            _status = TurretStatus.OutPlace;
        }
    }
}