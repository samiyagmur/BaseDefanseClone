using Enums;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public class ObstacleManager : MonoBehaviour
    {
        private IStateChangeble obstacleMovementController;

        private void Awake() => obstacleMovementController = GetComponent<IStateChangeble>();

        public void IsHitEnterGate() => obstacleMovementController.ChangeGateState(GateState.open);

        public void IsHitExitGate() => obstacleMovementController.ChangeGateState(GateState.close);
    }
}