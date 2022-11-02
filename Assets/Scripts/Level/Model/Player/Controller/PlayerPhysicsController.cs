using Enums;
using Interfaces;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables



        #region Serialized Variables,

        [SerializeField]
        private PlayerManager playerManager;

        #endregion Serialized Variables,

        #endregion Self Variables

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
            if (other.TryGetComponent(out GatePhysicsController obstaclePhysicsObject))
            {
                GateEnter(other);

                playerManager.SendToLayer(gameObject.layer);
            }

            if (other.TryGetComponent(out IDamager damager))
            {
                PlayerSignal.Instance.onTakePlayerDamage(damager.GetDamage());
            }

            //if (other.TryGetComponent(out BotGenareteController botGenareteController))
            //{
            //    Debug.Log("BotGenareteController");
            //    playerManager.PayToBuyableZone(botGenareteController.transform, botGenareteController.Price, botGenareteController.BotCreatType);
            //}
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GatePhysicsController obstaclePhysicsObject))
            {
                GateExit(other);
                playerManager.SendToLayer(gameObject.layer);
            }
        }

        public void ResetPlayerLayer()
        {
            gameObject.layer = LayerMask.NameToLayer("BaseDefense");//bakcaz
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