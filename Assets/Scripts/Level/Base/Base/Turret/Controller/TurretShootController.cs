using Enums;
using UnityEngine;

namespace Controllers
{
    public class TurretShootController : MonoBehaviour
    {
        private float rotateSpeed = 35;

        private void Spin(GattalingActivateStatus Activatable) => transform.Rotate((int)Activatable * Time.deltaTime * rotateSpeed, 0, 0);

        public void ActiveGattaling() => Spin(GattalingActivateStatus.Active);

        public void DeactiveGattaling() => Spin(GattalingActivateStatus.Pasive);
    }
}