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
    public class FireToTurret : MonoBehaviour,IReleasePoolObject, IGetPoolObject
    {
        [SerializeField]
        private float _rockedSpeed;
        private GameObject _rocked;

        internal  void FireToRocked()
        {
            _rocked = GetObject(PoolType.TurretRocket);//RockedHolder

            Rigidbody _rigidbody = _rocked.GetComponent<Rigidbody>();

            _rocked.transform.position = transform.position;
            _rocked.transform.rotation = transform.rotation;

            _rigidbody.AddForce(transform.forward * 20, ForceMode.VelocityChange);
            //poola gidecek

            //ammoStackten cekecek
        }

        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);

        }
        public void ReleaseObject(GameObject rocked, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName, rocked);
        }

    }   
}