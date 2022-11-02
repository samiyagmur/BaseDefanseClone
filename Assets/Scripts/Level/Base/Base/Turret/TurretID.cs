using Enums;
using UnityEngine;

namespace Assinger
{
    public class TurretID : MonoBehaviour
    {
        [SerializeField]
        private TurretId turretId;

        public TurretId GetId { get => turretId; }
    }
}