using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers;
using Enums;
using Abstraction;

namespace Managers
{
    public class ObstacleManager : MonoBehaviour
    {
        private ObstacleAbstraction obstacleMovementController;

        private void Awake()
        {
            obstacleMovementController = new ObstacleMovementController();
        }

        public void IsHitEnterPlayer() => obstacleMovementController.ChangeGateState(GateState.open);

        public void IsHitExitPlayer() => obstacleMovementController.ChangeGateState(GateState.close);

    }

}