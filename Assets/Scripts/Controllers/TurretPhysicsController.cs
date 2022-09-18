using Interfaces;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class TurretPhysicsController : MonoBehaviour,IPlayerHitAble
    {

        private bool IsHitPlayer;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Base"))
            {
                IsHitPlayer = true;
            }
        }
        public bool HitPlayer()
        {
            return IsHitPlayer;
        }

    }
}