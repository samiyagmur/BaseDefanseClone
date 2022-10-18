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
    public class FireToTurret : MonoBehaviour,IReleasePoolObject
    {
        [SerializeField]
        private float _rockedSpeed;
        private GameObject _rocked;

        bool IsRockedAlive;

        internal async void FireToRocked(GameObject rocked)
        {
            IsRockedAlive = _rocked != null;

            Rigidbody _rigidbody = _rocked.GetComponent<Rigidbody>();

            _rocked.transform.position = transform.position;
            _rocked.transform.rotation = transform.rotation;

            _rigidbody.AddForce(transform.forward * 20, ForceMode.VelocityChange);

            if (IsRockedAlive)
            {
                await Task.Delay(5000);
                ReleaseObject(_rocked,PoolType.TurretRocket);
            }
            //poola gidecek
        }

        public void ReleaseObject(GameObject rocked, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName, rocked);
        }

    }   
}