using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Enums;

namespace Controllers 
{
    public class ObstacleMovementController : MonoBehaviour
    {
        public void ChangeGateState(GateState state) => MoveRoate((float)state);
        private void MoveRoate(float rotate) => transform.GetChild(0).transform.DORotate(new Vector3(rotate, 90, 0), 1.5f);
    }

}

