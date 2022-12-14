using DG.Tweening;
using Enums;
using Interfaces;
using UnityEngine;

namespace Controllers
{
    public class ObstacleMovementController : MonoBehaviour, IStateChangeble
    {
        public void ChangeGateState(GateState state) => MoveRoate((float)state);

        public void MoveRoate(float rotate) => transform.GetChild(0).transform.DORotate(new Vector3(rotate, 90, 0), 1f);
    }
}