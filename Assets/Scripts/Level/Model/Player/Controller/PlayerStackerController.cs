using Abstraction;
using DG.Tweening;
using Enums;
using Interfaces;
using Signals;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Controller
{
    public class PlayerStackerController : AStacker, IGetPoolObject, IReleasePoolObject
    {
        [SerializeField] private List<Vector3> positionList;

        [SerializeField] private float radiusAround;

        private float _stackDelay = 0.5f;

        private Sequence _getStackSequence;

        private int _stackListOrder = 0;

        private int _stackListConstCount;

        private bool _canRemove = true;

        private void Awake()
        {
            DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(500, 125);
        }

        private new List<GameObject> StackList
        {
            get => base.StackList;
            set => base.StackList = value;
        }

        public override void SetStackHolder(Transform otherTransform)
        {
            otherTransform.SetParent(transform);
        }

        public override void GetStack(GameObject stackableObj)
        {
            _getStackSequence = DOTween.Sequence();
            var randomBouncePosition = CalculateRandomAddStackPosition();
            var randomRotation = CalculateRandomStackRotation();

            _getStackSequence.Append(stackableObj.transform.DOLocalMove(randomBouncePosition, .5f));
            _getStackSequence.Join(stackableObj.transform.DOLocalRotate(randomRotation, .5f)).OnComplete(() =>
            {
                stackableObj.transform.rotation = Quaternion.LookRotation(transform.forward);

                StackList.Add(stackableObj);

                stackableObj.transform.DOLocalMove(positionList[StackList.Count - 1], 0.3f);

                stackableObj.transform.localRotation = new Quaternion(0, 0, 0, 0).normalized;
            });
        }

        public void OnRemoveAllStack()
        {
            if (!_canRemove)
                return;
            _canRemove = false;
            _stackListConstCount = StackList.Count;
            RemoveAllStack();
            CoreGameSignals.Instance.onUpdateMoneyScore(_stackListConstCount * 10);
        }

        private async void RemoveAllStack()
        {
            if (StackList.Count == 0)
            {
                _canRemove = true;

                return;
            }
            if (StackList.Count >= _stackListConstCount - 6)
            {
                RemoveStackAnimation(StackList[StackList.Count - 1]);
                StackList.TrimExcess();
                await Task.Delay(201);
                RemoveAllStack();
            }
            else
            {
                for (int i = 0; i < StackList.Count; i++)
                {
                    RemoveStackAnimation(StackList[i]);
                    StackList.TrimExcess();
                }
                _canRemove = true;
            }
        }

        private void RemoveStackAnimation(GameObject removedStack)
        {
            _getStackSequence = DOTween.Sequence();
            var randomRemovedStackPosition = CalculateRandomRemoveStackPosition();
            var randomRemovedStackRotation = CalculateRandomStackRotation();

            _getStackSequence.Append(removedStack.transform.DOLocalMove(randomRemovedStackPosition, .2f));
            _getStackSequence.Join(removedStack.transform.DOLocalRotate(randomRemovedStackRotation, .2f)).OnComplete(() =>
            {
                removedStack.transform.rotation = Quaternion.LookRotation(transform.forward);

                StackList.Remove(removedStack);
                removedStack.transform.DOLocalMove(transform.localPosition, .1f).OnComplete(() =>
                {
                    removedStack.transform.SetParent(null);
                    removedStack.SetActive(false);
                });
            });
        }

        public override void ResetStack(IStackable stackable)
        {
        }

        public void GetStackPositions(List<Vector3> stackPositions)
        {
            positionList = stackPositions;
        }

        private Vector3 CalculateRandomAddStackPosition()
        {
            var randomHeight = Random.Range(0.1f, 3f);
            var randomAngle = Random.Range(230, 310);
            var rad = randomAngle * Mathf.Deg2Rad;
            return new Vector3(radiusAround * Mathf.Cos(rad),
                transform.parent.position.y + randomHeight, -radiusAround * Mathf.Sin(rad));
        }

        private Vector3 CalculateRandomRemoveStackPosition()
        {
            var randomHeight = Random.Range(0.1f, 3f);
            var randomAngle = Random.Range(1, 179);
            var rad = randomAngle * Mathf.Deg2Rad;
            return new Vector3(radiusAround * Mathf.Cos(rad),
                transform.parent.position.y + randomHeight, radiusAround * Mathf.Sin(rad));
        }

        private Vector3 CalculateRandomStackRotation()
        {
            var randomRotationX = Random.Range(-90, 90);
            var randomRotationY = Random.Range(-90, 90);
            var randomRotationZ = Random.Range(-90, 90);
            return new Vector3(randomRotationX, randomRotationY, randomRotationZ);
        }

        public void PaymentStackAnimation(Transform transform, PoolType poolname)
        {
            _getStackSequence = DOTween.Sequence();
            var randomBouncePosition = CalculateRandomAddStackPositionWithObjTransform();
            var randomRotation = CalculateRandomStackRotation();
            var moneyObj = GetObject(poolname);
            moneyObj.transform.position = this.transform.parent.transform.position;
            moneyObj.GetComponent<Collider>().enabled = false;
            _getStackSequence.Append(moneyObj.transform.DOMove(randomBouncePosition, .5f));
            _getStackSequence.Join(moneyObj.transform.DOLocalRotate(randomRotation, .5f)).OnComplete(() =>
            {
                moneyObj.transform.rotation = Quaternion.LookRotation(transform.forward);

                moneyObj.transform.DOMove(transform.position, 0.3f).OnComplete(() => ReleaseObject(moneyObj, poolname));
            });
        }

        private Vector3 CalculateRandomAddStackPositionWithObjTransform()
        {
            var randomHeight = Random.Range(0.1f, 3f);
            var randomAngle = Random.Range(230, 310);
            var rad = randomAngle * Mathf.Deg2Rad;
            return new Vector3(transform.parent.position.x + radiusAround * Mathf.Cos(rad),
                transform.parent.position.y + randomHeight, transform.parent.position.z + -radiusAround * Mathf.Sin(rad));
        }

        public async void ResetStack()
        {
            if (StackList.Count == 0)
            {
                return;
            }
            StackList[0].transform.SetParent(null);
            StackList.Remove(StackList[0]);
            StackList.TrimExcess();
            await Task.Delay(10);
            ResetStack();
        }

        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool.Invoke(poolName);
        }

        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool.Invoke(poolName, obj);
        }
    }
}