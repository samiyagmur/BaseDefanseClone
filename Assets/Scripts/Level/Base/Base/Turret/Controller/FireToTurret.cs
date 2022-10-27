using Enums;
using Interfaces;
using Signals;
using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using Managers;

namespace Controllers
{
    public class FireToTurret : MonoBehaviour,IReleasePoolObject,IGetPoolObject
    {
        [SerializeField]
        private float rockedSpeed;

        [SerializeField]
        private TurretManager turretManager;

        Rigidbody _rigidbody;

        private GameObject _rocked;

        private Sequence _moveToTurret;
        private float _timer;

        public  void LoadMagazine(TurretId turretKey, bool IsDeadListEmty, SphereCollider attackerTurretCollider)
        {
            _moveToTurret = DOTween.Sequence();
            if (attackerTurretCollider.radius <= 0) return;

          
            if (turretManager.GetToStackStatus(turretKey) == false);

            if (!IsDeadListEmty) return;

            _timer -= Time.deltaTime;

            if (_timer < 0)
            {
                _timer = 4f;

                FireToRocked(turretManager.GetToRocked(turretKey));
            }

        }
        internal  void FireToRocked(GameObject ammo)
        {
            

            if (ammo == false) return;
       
            _moveToTurret.Append(ammo.transform.DOMove(transform.position, 0.8f));

            _moveToTurret.Join(ammo.transform.DOScale(Vector3.zero, 0.8f));

            _moveToTurret.Play();

            _rocked = GetObject(PoolType.TurretRocket);

            _rigidbody=_rocked.GetComponent<Rigidbody>();

            _rocked.transform.position = transform.position;

            _rocked.transform.rotation = transform.rotation;

            _rigidbody.AddForce(transform.forward * 20, ForceMode.VelocityChange);

        }

        public void ReleaseObject(GameObject rocked, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName, rocked);
        }

        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool(PoolType.TurretRocket);
        }







    }   
}