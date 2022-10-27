using Data.UnityObject;
using DG.Tweening;
using Enums;
using Managers;
using Signals;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Controllers
{
    public class AmmoDropZoneController :MonoBehaviour
    {

        [SerializeField]
        private List<GameObject> _ammoWorkerStackList;
        [SerializeField]
        private List<GameObject> _turretStack=new List<GameObject>();
        List<Vector3> _gridPosList;
        private int _currentCount=0;
        private int _count =0;

        private Sequence _ammoStackingMovement;

        internal void LoadGrid(List<Vector3> gridList)
        {
            _gridPosList=gridList;
        }

        public async void AddStack(List<GameObject> ammoWorkerStackList)
        {   
             int _counter = 0;

             ammoWorkerStackList.Reverse();

            _ammoWorkerStackList = ammoWorkerStackList;

            _ammoStackingMovement = DOTween.Sequence();

            
                while ( _counter < _gridPosList.Count)
                {   
                    if (_currentCount < _gridPosList.Count)
                    {
                        if (_count < _ammoWorkerStackList.Count)
                        {
                            _ammoWorkerStackList[_count].transform.SetParent(transform);

                            GameObject bullets = _ammoWorkerStackList[_count];

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

                            _turretStack.Add(_ammoWorkerStackList[_count]);

                            await Task.Delay(100);

                            _currentCount++;
                            
                            _count++;
                            
                         }

                        else
                        {
                            _count = 0;
                       
                            AmmoManagerSignals.Instance.onSetAmmoStackStatus.Invoke(AmmoStackStatus.Empty);

                            _ammoWorkerStackList.Clear();

                            _ammoWorkerStackList.TrimExcess();

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
            if (_turretStack.Count <= 0) return null;

            return _turretStack[_turretStack.Count - 1];
        }
        public void UpDateList()
        {
            if (_turretStack.Count <= 0) return;
            _turretStack.RemoveAt(_turretStack.Count - 1);
            _turretStack.TrimExcess();
            _currentCount--;
        }
        public int GetCurrentCount()
        {
            return _currentCount;
        }
    }
}