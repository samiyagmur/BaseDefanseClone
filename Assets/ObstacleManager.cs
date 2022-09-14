using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers;
using Enums;

namespace Managers
{
    public class ObstacleManager : MonoBehaviour
    {

        [SerializeField]
        ObstacleMovementController obstacleMovementController;
        public void IsHitEnterPlayer() => obstacleMovementController.ChangeGateState(GateState.open);
        public void IsHitExitPlayer() => obstacleMovementController.ChangeGateState(GateState.close);

    }

}