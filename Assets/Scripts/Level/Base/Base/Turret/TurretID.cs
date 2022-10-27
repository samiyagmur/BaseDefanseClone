using Controllers;
using Controllers.AIControllers;
using Enums;
using Interfaces;
using System.Collections;
using UnityEngine;

namespace Assinger
{
    public class TurretID:MonoBehaviour
    {
        [SerializeField]
        private TurretId turretId;

        public TurretId GetId { get => turretId;}
    }
}