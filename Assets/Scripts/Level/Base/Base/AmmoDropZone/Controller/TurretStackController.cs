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
    public class TurretStackController :MonoBehaviour
    {

        [SerializeField]
        private List<GameObject> _ammoWorkerStackList;
        [SerializeField]
        private List<GameObject> _turretContayner=new List<GameObject>();

        private int _currentCount=0;
        private int _count =0;

        private Sequence _ammoStackingMovement;
        private float _timer;

        public  void AddStack(List<Vector3> gridPosList, List<GameObject> ammoWorkerStackList)
        {
             ammoWorkerStackList.Reverse();

            _ammoWorkerStackList = ammoWorkerStackList;

            _ammoStackingMovement = DOTween.Sequence();

            for (int i = 0; i < gridPosList.Count; i++)
            {

              
                if (_currentCount < gridPosList.Count)
                {
                    if (_count < _ammoWorkerStackList.Count)
                    {
                        
                            _timer = 0f;

                         

                            _ammoWorkerStackList[_count].transform.SetParent(transform);

                            GameObject bullets = _ammoWorkerStackList[_count];
                        
                            Vector3 endPosOnTurretStack = transform.localPosition + gridPosList[_currentCount];
                        
                            _ammoStackingMovement.Append(bullets.transform.
                            DOLocalMove(new Vector3(Random.Range(-2, 2), endPosOnTurretStack.y +
                            Random.Range(4, 6), bullets.transform.localPosition.z + 3f), 0.4f).
                            OnComplete(() =>
                            {
                                bullets.transform.
                            DOMove(new Vector3(endPosOnTurretStack.x, endPosOnTurretStack.y + 0.25f, endPosOnTurretStack.z), 0.4f);
                            }));

                            _ammoStackingMovement.Join(bullets.transform.DOLocalRotate(new Vector3(Random.Range(-179, 179), Random.Range(-179, 179), Random.Range(-179, 179)), 0.6f).

                            OnComplete(() => bullets.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f)));

                            _ammoStackingMovement.Play();

                            _turretContayner.Add(_ammoWorkerStackList[_count]);

                         
                        
                            _currentCount++;

                            _count++;

                    }
                    else
                    {                           
                        _count = 0;

                        AmmoManagerSignals.Instance.onSetAmmoStackStatus.Invoke(AmmoStackStatus.Empty);

                        _ammoWorkerStackList.Clear();

                        _ammoWorkerStackList.TrimExcess();

                        return;
                    }
                }  
            }
        }
        
        public void RemoveStack()
        {


        }

        public int GetCurrentCount()
        {
            return _currentCount;
        }
    }
}