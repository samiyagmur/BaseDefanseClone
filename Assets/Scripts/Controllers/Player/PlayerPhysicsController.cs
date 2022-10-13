using Datas.ValueObject;
using Interfaces;
using Managers;
using System.Collections;
using UnityEngine;
using Enums;
using System;
using Enums.GameStates;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
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
            {
                _physicsData._playerLayersType = AreaType.BattleOn;
                playerManager.CheckAreaStatus(AreaType.BattleOn);
            }
            else
            {
                _physicsData._playerLayersType = AreaType.BaseDefense;
                gameObject.layer = LayerMask.NameToLayer(_physicsData._playerLayersType.ToString());
                playerManager.CheckAreaStatus(AreaType.BaseDefense);
            }
                
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(TurretPhysicsController), out Component getterTurretObject))
            {

                playerManager.IsEnterTurret(getterTurretObject.gameObject);


            }

            if (other.TryGetComponent(typeof(ObstaclePhysicsController), out Component obstaclePhysicsObject))
            {
                ChangeLayerMask();
            }


        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(typeof(ObstaclePhysicsController), out Component obstaclePhysicsObject))
            {
                ChangeLayerMask();
            }

        }

    }
}