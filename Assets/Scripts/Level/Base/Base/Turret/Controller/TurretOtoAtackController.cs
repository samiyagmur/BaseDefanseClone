using Assinger;
using Datas.ValueObject;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class TurretOtoAtackController : MonoBehaviour
    {
        #region Self Variabels

        #region Private Variables

        [ShowInInspector]
        private Queue<GameObject> _deadList = new Queue<GameObject>();

        private GameObject _botTarget;

        private TurretOtoAtackData _turretOtoAtackDatas;

        private Tweener _tween;

        #endregion Private Variables

        #endregion Self Variabels

        [SerializeField]
        private TurretID turretID;

        public void SetOtoAtackDatas(TurretOtoAtackData turretOtoAtackDatas) => _turretOtoAtackDatas = turretOtoAtackDatas;

        public void AddDeathList(GameObject enemy)
        {
            _deadList.Enqueue(enemy);

            _botTarget = _deadList.Peek();
        }

        public void RemoveDeathList(GameObject gameObject)
        {
            Debug.Log(gameObject);
            if (!_deadList.Contains(gameObject)) return;

            if (_deadList.Count <= 0) return;
            _deadList.Dequeue();

            if (_deadList.Count <= 0) return;
            _botTarget = _deadList.Peek();
        }

        public void FollowToEnemy()
        {
            if (_deadList.Count <= 0) return;

            ArangeRotateRotation(_botTarget.transform);
        }

        private void ArangeRotateRotation(Transform _movementDirection)
        {
            if (_movementDirection.position == Vector3.zero) return;

            Vector3 horizontalRotation = new Vector3(_movementDirection.position.x, 0, _movementDirection.position.z + 0.5f);

            Vector3 _relativePos = horizontalRotation - transform.position;

            Quaternion _toRotation = Quaternion.LookRotation(_relativePos);

            _tween.ChangeEndValue(_toRotation, true);
        }

        public void RotateTurret()
        {
            if (_deadList.Count <= 0) return;
            _tween = transform.DORotateQuaternion(_botTarget.transform.rotation, 0.499f).SetAutoKill(true);
        }

        public bool GetTargetStatus()
        {
            return _deadList.Count >= 0;
        }

        public GameObject GetTarget()
        {
            return _deadList.Peek();
        }
    }
}