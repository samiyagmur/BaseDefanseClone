using AIBrain;
using Managers;
using System.Collections;
using UnityEngine;
using Interfaces;
using Enums;

namespace Controllers
{
    public class TurretStackPhysicsControl : MonoBehaviour
    {
        [SerializeField]
        private TurretKey turretKey;

        public TurretKey TurretKey { get => turretKey; }

        private void OnTriggerEnter(Collider other)
        {

        }
  
    }
}