using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers;
using Enums;
using Abstraction;
using Signals;

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