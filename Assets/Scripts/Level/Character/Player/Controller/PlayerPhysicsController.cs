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
            if (Math.Abs(transform.rotation.eulerAngles.y) < _physicsData._desitionAngle)
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
            
            if (other.TryGetComponent(typeof(ObstaclePhysicsController), out Component obstaclePhysicsObject))
            {
                ChangeLayerMask();
            }

            if (other.TryGetComponent(out TurretPhysicsController turretPhysicsController))
            {
                playerManager.SetTurretAnim(true);
            }


            }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(typeof(ObstaclePhysicsController), out Component obstaclePhysicsObject))
            {
                ChangeLayerMask();
            }
            if (other.TryGetComponent(out TurretPhysicsController turretPhysicsController))
            {
                playerManager.SetTurretAnim(false);
            }

        }

    }
}
//#region Self Variables

//#region Public Variables

//#endregion

//#region Serialized Variables,

//[SerializeField]
//private PlayerManager playerManager;

//#endregion

//#region Private Variables

//#endregion

//#endregion
//private void OnTriggerEnter(Collider other)
//{
//    if (other.TryGetComponent(out GatePhysicsController physicsController))
//    {
//        GateEnter(other);
//    }
//    if (other.TryGetComponent(out TurretPhysicsController turretPhysicsController))
//    {
//        playerManager.SetTurretAnim(true);
//    }
//}
//private void OnTriggerExit(Collider other)
//{
//    if (other.TryGetComponent(out GatePhysicsController physicsController))
//    {
//        GateExit(other);
//    }
//    if (other.TryGetComponent(out TurretPhysicsController turretPhysicsController))
//    {
//        playerManager.SetTurretAnim(false);
//    }
//}
//private void GateEnter(Collider other)
//{
//    var playerIsGoingToFrontYard = other.transform.position.z > transform.position.z;
//    gameObject.layer = LayerMask.NameToLayer("PlayerBase");
//    playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaType.BattleOn : AreaType.BaseDefense);
//}
//private void GateExit(Collider other)
//{
//    var playerIsGoingToFrontYard = other.transform.position.z < transform.position.z;
//    gameObject.layer = LayerMask.NameToLayer(playerIsGoingToFrontYard ? "PlayerFrontYard" : "PlayerBase");
//    playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaType.BattleOn : AreaType.BaseDefense);
//    if (!playerIsGoingToFrontYard) return;
//    playerManager.HasEnemyTarget = false;
//    playerManager.EnemyList.Clear();
//}