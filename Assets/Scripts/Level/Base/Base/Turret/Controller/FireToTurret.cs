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
        private float _rockedSpeed;

        [SerializeField]
        private TurretManager turretManager;

        private GameObject _rocked;

        private Sequence moveToTurret;
        private float _timer;

        public  void LoadMagazine(TurretKey turretKey, bool IsDeadListUnEmty, SphereCollider attackerTurretCollider)
        {
            if (attackerTurretCollider.radius <= 0) return;

            if (IsDeadListUnEmty==false) return;

            if (turretManager.GetToStackInfo(turretKey) == false);

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

            moveToTurret = DOTween.Sequence();

      
            moveToTurret.Append(ammo.transform.DOMove(transform.position, 0.8f));

            moveToTurret.Join(ammo.transform.DOScale(Vector3.zero, 0.8f));

            moveToTurret.Play().OnComplete(() => ReleaseObject(ammo, PoolType.Ammo));


            _rocked = GetObject(PoolType.TurretRocket);

            Rigidbody _rigidbody = _rocked.GetComponent<Rigidbody>();

            _rocked.transform.position = transform.position;
            _rocked.transform.rotation = transform.rotation;

            _rigidbody.AddForce(transform.forward * 20, ForceMode.VelocityChange);

            //if (_rocked.activeInHierarchy)
            //{
            //    _rocked.transform.DOScale(_rocked.transform.localScale, 3f).OnComplete(()
            //    => ReleaseObject(ammo, PoolType.Ammo));
            //}


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