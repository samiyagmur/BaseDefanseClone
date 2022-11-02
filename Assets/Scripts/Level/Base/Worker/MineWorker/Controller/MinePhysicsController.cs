using Managers;
using UnityEngine;

namespace Controllers
{
    public class MinePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField]
        private MineManager mineManager;

        [SerializeField]
        private SphereCollider lureCollider;

        [SerializeField]
        private Collider marketCollider;

        [SerializeField]
        private SphereCollider explosionCollider;

        #endregion Serialized Variables

        #region Private Variables

        private int _initalLureSphereSize = 20;
        private int _initalExplosionSphereSize = 10;

        private float _timer;
        private float _payOffset = 0.1f;

        #endregion Private Variables

        #endregion Self Variables

        private void Awake()
        {
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_timer > _payOffset)
                {
                    //Revize edecegim button modulunu yapinca
                    mineManager.PayGemToMine();
                    _timer = 0;
                }
                else
                {
                    _timer += Time.deltaTime;
                }
            }
        }

        public void LureColliderState(bool _state)
        {
            if (_state)
            {
                //gameObject.tag = "MineLure";
                lureCollider.radius = _initalLureSphereSize;
                lureCollider.enabled = true;
            }
            else
            {
                lureCollider.radius = .1f;
                lureCollider.enabled = false;
            }
        }

        public void ExplosionColliderState(bool _state)
        {
            if (_state)
            {
                //gameObject.tag = "MineExplosion";
                lureCollider.radius = _initalExplosionSphereSize;
                explosionCollider.enabled = true;
            }
            else
            {
                lureCollider.radius = .1f;
                explosionCollider.enabled = false;
                //Satin alma veonun etkilesimi revize edilecek
            }
        }
    }
}