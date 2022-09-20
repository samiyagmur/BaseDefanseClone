using Datas.ValueObject;
using Interfaces;
using Managers;
using System.Collections;
using UnityEngine;
using Enums;
using System;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour, IGeterGameObject
    {

        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables,
        [SerializeField]
        PlayerManager playerManager;
        #endregion

        #region Private Variables
        private PlayerPhysicsData _physicsData;

        #endregion

        #endregion


        public void SetPhysicsData(PlayerPhysicsData physicsData)
        {
            _physicsData = physicsData;
        }

        public void ChangeLayerMask()
        {
            if (Math.Abs(transform.rotation.eulerAngles.y) < _physicsData._disitionAngle)
                _physicsData._playerLayersType = LayersType.BattleField;
            else
                _physicsData._playerLayersType = LayersType.Base;
            gameObject.transform.parent.transform.gameObject.layer = LayerMask.NameToLayer(_physicsData._playerLayersType.ToString());
        }



        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(IGeterGameObject),out Component getterTurretObject))
            {
                playerManager.IsEnterTurret(getterTurretObject.gameObject);
                
            }

            if (other.gameObject.layer==LayerMask.NameToLayer("AmmoCreater"))
            {
                playerManager.IsEnterAmmoCreater(other.gameObject.transform);
            }


        }


        private void OnTriggerExit(Collider other)
        {
           

        }

    }
}