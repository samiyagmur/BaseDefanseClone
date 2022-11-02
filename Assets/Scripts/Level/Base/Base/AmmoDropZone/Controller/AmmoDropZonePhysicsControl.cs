using Enums;
using UnityEngine;

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