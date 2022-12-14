using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class BulletMovementController : MonoBehaviour
    {
        #region Self Variables



        #region Serialized Variables

        [SerializeField]
        private new Rigidbody rigidbody;

        private Transform _playerTransform;
        private const float _fireDelay = 0.05f;
        private const float _fireSpeed = 70;

        #endregion Serialized Variables

        #endregion Self Variables

        private void OnEnable()
        {
            DOVirtual.DelayedCall(_fireDelay, () => FireBullet());
        }

        private void FireBullet()
        {
            rigidbody.AddRelativeForce(Vector3.forward * _fireSpeed, ForceMode.VelocityChange);
        }

        private void OnDisable()
        {
            rigidbody.velocity = Vector3.zero;
        }

        internal void SetPlayerTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }
    }
}