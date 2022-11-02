using DG.Tweening;
using Enums;
using Signals;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Controllers
{
    public class AmmoDropZoneController : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> ammoWorkerStackList;

        [SerializeField]
        private List<GameObject> turretStack = new List<GameObject>();

        private List<Vector3> _gridPosList;
        private int _currentCount = 0;
        private int _count = 0;

        private Sequence _ammoStackingMovement;

        internal void LoadGrid(List<Vector3> gridList)
        {
            _gridPosList = gridList;
        }

        public async void AddStack(List<GameObject> ammoWorkerStackList)
        {
            int _counter = 0;

            ammoWorkerStackList.Reverse();

            this.ammoWorkerStackList = ammoWorkerStackList;

            _ammoStackingMovement = DOTween.Sequence();

            while (_counter < _gridPosList.Count)
            {
                if (_currentCount < _gridPosList.Count)
                {
                    if (_count < this.ammoWorkerStackList.Count)
                    {
                        this.ammoWorkerStackList[_count].transform.SetParent(transform);

                        GameObject bullets = this.ammoWorkerStackList[_count];

                        Vector3 endPosOnTurretStack = transform.localPosition + _gridPosList[_currentCount];

                        _ammoStackingMovement.Append(bullets.transform.
                            DOLocalMove(new Vector3(Random.Range(-2, 2), endPosOnTurretStack.y +
                            Random.Range(4, 6), bullets.transform.localPosition.z + 3f), 0.4f).
                            OnComplete(() =>
                            {
                                bullets.transform.DOMove(new Vector3(endPosOnTurretStack.x, endPosOnTurretStack.y + 0.25f, endPosOnTurretStack.z), 0.4f);
                            }));

                        _ammoStackingMovement.Join(bullets.transform.DOLocalRotate(new Vector3(Random.Range(-179, 179), Random.Range(-179, 179), Random.Range(-179, 179)), 0.6f).

                            OnComplete(() => bullets.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f)));

                        _ammoStackingMovement.Play();

                        turretStack.Add(this.ammoWorkerStackList[_count]);

                        await Task.Delay(100);

                        _currentCount++;

                        _count++;
                    }
                    else
                    {
                        _count = 0;

                        AmmoManagerSignals.Instance.onSetAmmoStackStatus.Invoke(AmmoStackStatus.Empty);

                        this.ammoWorkerStackList.Clear();

                        this.ammoWorkerStackList.TrimExcess();

                        break;
                    }
                }
                _counter++;
                if (_currentCount >= _gridPosList.Count)
                    AmmoManagerSignals.Instance.onSetAmmoStackStatus.Invoke(AmmoStackStatus.Full);
            }
        }

        public GameObject RemoveToStack()
        {
            if (turretStack.Count <= 0) return null;

            return turretStack[turretStack.Count - 1];
        }

        public void UpDateList()
        {
            if (turretStack.Count <= 0) return;
            turretStack.RemoveAt(turretStack.Count - 1);
            turretStack.TrimExcess();
            _currentCount--;
        }

        public int GetCurrentCount()
        {
            return _currentCount;
        }
    }
}