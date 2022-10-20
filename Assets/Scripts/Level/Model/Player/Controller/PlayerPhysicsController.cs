using Managers;
using UnityEngine;
using Enums;
using System;
using Interfaces;
using Signals;

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


        #endregion

        #endregion

        private void GateEnter(Collider other)
        {
            var playerIsGoingToFrontYard = other.transform.position.z > transform.position.z;
            gameObject.layer = LayerMask.NameToLayer("BaseDefense");
            playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaType.BattleOn : AreaType.BaseDefense);
            playerManager.CloseHealtBar();
        }
        private void GateExit(Collider other)
        {
            var playerIsGoingToFrontYard = other.transform.position.z < transform.position.z;
            gameObject.layer = LayerMask.NameToLayer(playerIsGoingToFrontYard ? "BattleOn" : "BaseDefense");
            playerManager.CheckAreaStatus(playerIsGoingToFrontYard ? AreaType.BattleOn : AreaType.BaseDefense);
            if (!playerIsGoingToFrontYard) return;
            playerManager.HasEnemyTarget = false;
            playerManager.EnemyList.Clear();
            playerManager.OpenHealtBar();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ObstaclePhysicsController obstaclePhysicsObject))
            {
                GateEnter(other);
            }

            if (other.TryGetComponent(out TurretPhysicsController turretPhysicsController))
            {
                playerManager.SetTurretAnim(true);
            }

            if (other.TryGetComponent(out IDamager damager))
            {   
                PlayerSignal.Instance.onTakePlayerDamage(damager.GetDamage());
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ObstaclePhysicsController obstaclePhysicsObject))
            {
                GateExit(other);
            }
            if (other.TryGetComponent(out TurretPhysicsController turretPhysicsController))
            {
                playerManager.SetTurretAnim(false);
            }

        }

        public void ResetPlayerLayer()
        {
            gameObject.layer = LayerMask.NameToLayer("BaseDefense");//bakcaz
            Debug.Log("bak");
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