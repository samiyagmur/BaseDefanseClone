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

        #endregion

        #region Private Variables
        private PlayerLayersType _playerLayersType;
        private int _disitionAngle=90;
       // private HorizontalInputParams _inputParams;

        #endregion

        #endregion
        public void ChangeLayerMask()
        {
            if (Math.Abs( transform.rotation.eulerAngles.y) < _disitionAngle)
                _playerLayersType = PlayerLayersType.BattleField;   
            else
                _playerLayersType = PlayerLayersType.Base;
            gameObject.transform.parent.transform.gameObject.layer = LayerMask.NameToLayer(_playerLayersType.ToString());
        }

    }
}