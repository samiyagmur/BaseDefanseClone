using Enums;
using Interfaces;
using Signals;
using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Controllers
{
    public class FireToTurret : MonoBehaviour,IReleasePoolObject, IGetPoolObject
    {
        [SerializeField]
        private float _rockedSpeed;
        private GameObject _rocked;

        internal   void FireToRocked()
        {   
            Debug.Log("FireToRocked");

            _rocked = GetObject(PoolType.TurretRocket);//RockedHolder

            _rocked.transform.position = transform.position;
            _rocked.transform.rotation = transform.rotation;

            _rocked.transform.Translate(new Vector3(0,0,_rockedSpeed*Time.deltaTime),Space.World);
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