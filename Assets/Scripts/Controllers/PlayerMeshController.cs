using Enums;
using Keys;
using System;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class PlayerMeshController : MonoBehaviour
    {

        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables,

        [SerializeField] private Transform manager;

        #endregion

        #region Private Variables
        private PlayerLayersType _playerLayersType;
        #endregion

        #endregion
        public void LookRotation(HorizontalInputParams inputParams)
        {
            var movementDirection = new Vector3(inputParams.MovementVector.x, 0, inputParams.MovementVector.y);
            if (movementDirection == Vector3.zero) return;
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            manager.rotation = Quaternion.RotateTowards(manager.rotation, toRotation, 30);
        }

        public void ChangeLayerMask()
        {
            if (Math.Abs(transform.rotation.eulerAngles.y) < 90)
                _playerLayersType = PlayerLayersType.BattleField;   
            else
                _playerLayersType = PlayerLayersType.Base;

           
            gameObject.transform.parent.transform.gameObject.layer = LayerMask.NameToLayer(_playerLayersType.ToString());
        }
    }
}