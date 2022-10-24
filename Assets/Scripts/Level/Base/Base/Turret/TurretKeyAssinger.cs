using Enums;
using System.Collections;
using UnityEngine;

namespace Assinger
{
    public class TurretKeyAssinger : MonoBehaviour
    {
        [SerializeField]
        private TurretKey turretKey;

        public TurretKey TurretKey { get => turretKey; }

    }
}