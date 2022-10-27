using AIBrain;
using Managers;
using System.Collections;
using UnityEngine;
using Interfaces;
using Enums;

namespace Controllers
{
    public class AmmoDropZonePhysicsControl : MonoBehaviour
    {
        [SerializeField]
        private TurretId turretKey;

        public TurretId TurretKey { get => turretKey; }

        private void OnTriggerEnter(Collider other)
        {

        }
  
    }
}