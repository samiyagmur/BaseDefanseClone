using Abstraction;
using DG.Tweening;
using Interfaces;
using Signals;
using System.Collections.Generic;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace Controllers
{
    public class GemStackerController : AStacker
    {
        public List<Vector3> PositionList = new List<Vector3>();

        [SerializeField] private float radiusAround;

        private Sequence _getStackSequence;

        private int _stackListConstCount;

        private bool _canRemove = true;

        private void Awake()
        {
            DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(500, 125);
        }

        public new List<GameObject> StackList
        {
            get => base.StackList;
            set => base.StackList = value;
        }

        public override void SetStackHolder(Transform otherTransform)
        {
            otherTransform.SetParent(transform);
        }

        public override void GetStack(GameObject stackableObj, Transform otherTransform)
        {
            SetStackHolder(otherTransform);
            _getStackSequence = DOTween.Sequence();
            var randomBouncePosition = CalculateRandomAddStackPosition();
            var randomRotation = CalculateRandomStackRotation();

            _getStackSequence.Append(stackableObj.transform.DOLocalMove(randomBouncePosition, .5f));
            _getStackSequence.Join(stackableObj.transform.DOLocalRotate(randomRotation, .5f)).OnComplete(() =>
            {
                stackableObj.transform.rotation = Quaternion.LookRotation(transform.forward);

                StackList.Add(stackableObj);

                stackableObj.transform.DOLocalMove(PositionList[StackList.Count - 1], 0.3f);
            });
            if (PositionList.Count - 1 <= StackList.Count)
            {
                DropzoneSignals.Instance.onDropZoneFull?.Invoke(true);
            }
        }

        public void OnRemoveAllStack(Transform targetTransform)
        {
            if (!_canRemove)
                return;

            _canRemove = false;

            _stackListConstCount = StackList.Count;

            RemoveAllStack(targetTransform);
        }

        private async void RemoveAllStack(Transform targetTransform)
        {
            if (StackList.Count == 0)
            {
                DropzoneSignals.Instance.onDropZoneFull?.Invoke(false);
                _canRemove = true;
                return;
            }

            if (StackList.Count > 0)
            {
                CoreGameSignals.Instance.onUpdateGemScore?.Invoke(1);
                RemoveStackAnimation(StackList[StackList.Count - 1], targetTransform);
                StackList.TrimExcess();
                if (StackList.Count % 9 == 0)
                {
                    await Task.Delay(100);
                }
                await Task.Delay(1);
                RemoveAllStack(targetTransform);
            }
        }

        private void RemoveStackAnimation(GameObject removedStack, Transform targetTransform)
        {
            _getStackSequence = DOTween.Sequence();
            var randomRemovedStackPosition = CalculateRandomRemoveStackPosition();
            var randomRemovedStackRotation = CalculateRandomStackRotation();
            _getStackSequence.Append(removedStack.transform.DOLocalMove(randomRemovedStackPosition, .1f));
            _getStackSequence.Join(removedStack.transform.DOLocalRotate(randomRemovedStackRotation, .1f)).OnComplete(() =>
            {
                removedStack.transform.rotation = Quaternion.LookRotation(targetTransform.forward);
                StackList.Remove(removedStack);

                removedStack.transform.DOMove(targetTransform.localPosition + new Vector3(0, targetTransform.localScale.y * 2, 0), .1f).OnComplete(() =>
                {
                    removedStack.transform.DOScale(Vector3.zero, 0.2f);
                    removedStack.transform.SetParent(null);
                    removedStack.SetActive(false);
                });
            });
        }

        public override void ResetStack(IStackable stackable)
        {
            base.ResetStack(stackable);
        }

        public void GetStackPositions(List<Vector3> stackPositions)
        {
            PositionList = stackPositions;
        }

        private Vector3 CalculateRandomAddStackPosition()
        {
            var randomHeight = Random.Range(0.1f, 3f);
            var randomAngle = Random.Range(230, 310);
            var rad = randomAngle * Mathf.Deg2Rad;
            return new Vector3(radiusAround * Mathf.Cos(rad),
                transform.position.y + randomHeight, -radiusAround * Mathf.Sin(rad));
        }

        private Vector3 CalculateRandomRemoveStackPosition()
        {
            var randomHeight = Random.Range(0.1f, 3f);
            var randomAngle = Random.Range(1, 179);
            var rad = randomAngle * Mathf.Deg2Rad;
            return new Vector3(radiusAround * Mathf.Cos(rad),
                transform.position.y + randomHeight, radiusAround * Mathf.Sin(rad));
        }

        private Vector3 CalculateRandomStackRotation()
        {
            var randomRotationX = Random.Range(-90, 90);
            var randomRotationY = Random.Range(-90, 90);
            var randomRotationZ = Random.Range(-90, 90);
            return new Vector3(randomRotationX, randomRotationY, randomRotationZ);
        }
    }
}