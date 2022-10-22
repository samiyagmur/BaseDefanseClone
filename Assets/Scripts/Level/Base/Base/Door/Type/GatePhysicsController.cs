using Managers;
using System.Collections;
using UnityEngine;


namespace Controllers
{
    public class GatePhysicsController : MonoBehaviour
    {

        [SerializeField]
        ObstacleManager obstacleManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerPhysicsController playerPhysicsController)) 
                obstacleManager.IsHitEnterGate();
            if (other.TryGetComponent(out MoneyWorkerPhysicController moneyWorkerPhysicController))
                obstacleManager.IsHitEnterGate();

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerPhysicsController playerPhysicsController)) 
                obstacleManager.IsHitExitGate();
            if (other.TryGetComponent(out MoneyWorkerPhysicController moneyWorkerPhysicController))
                obstacleManager.IsHitEnterGate();
        }
    }
}