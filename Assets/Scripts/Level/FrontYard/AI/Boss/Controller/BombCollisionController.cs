using System.Collections;
using UnityEngine;
using Interfaces;
using Signals;
using Enums;
using Managers;

namespace Controllers
{
    public class BombCollisionController : MonoBehaviour, IReleasePoolObject
    {
        [SerializeField]
        private ParticleSystem bombParticle;

        [SerializeField]
        private GameObject meshObj;

        [SerializeField]
        private Rigidbody bombRB;

        private const float DELAY = 1f;

        public void ReleaseObject(GameObject obj, PoolType poolType)
        {
            PoolSignals.Instance.onReleaseObjectFromPool(poolType, obj);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerManager>(out PlayerManager manager) || collision.gameObject.CompareTag("Ground"))
            {
                StartCoroutine(WaitForTask());
            }

        }

        private IEnumerator WaitForTask()
        {
            if (bombParticle)
            {
                bombParticle.Play();
            }

            bombRB.useGravity = false;
            meshObj.SetActive(false);

            yield return new WaitForSeconds(DELAY);

            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            this.transform.localScale = Vector3.one;
            meshObj.SetActive(true);
            ReleaseObject(this.gameObject, PoolType.Bomb);
        }


    } 
}
