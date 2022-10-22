using Enums;
using Interfaces;
using Signals;
using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

namespace Controllers
{
    public class FireToTurret : MonoBehaviour,IReleasePoolObject,IGetPoolObject
    {
        [SerializeField]
        private float _rockedSpeed;

        private GameObject _rocked;

        bool IsRockedAlive;

        private Sequence moveToTurret;
        internal async void FireToRocked(GameObject ammo)
        {
            moveToTurret = DOTween.Sequence();

            moveToTurret.Append(ammo.transform.DOMove(transform.position, 0.8f));

            moveToTurret.Join(ammo.transform.DOScale(Vector3.zero, 0.8f));

            moveToTurret.Play().OnComplete(() => ReleaseObject(ammo, PoolType.Ammo));

            _rocked=GetObject(PoolType.TurretRocket);

            Rigidbody _rigidbody = _rocked.GetComponent<Rigidbody>();

            _rocked.transform.position = transform.position;
            _rocked.transform.rotation = transform.rotation;

            _rigidbody.AddForce(transform.forward * 20, ForceMode.VelocityChange);

            IsRockedAlive = _rocked != null;

            if (IsRockedAlive)
            {
                await Task.Delay(5000);

                ReleaseObject(_rocked,PoolType.TurretRocket);
            }

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