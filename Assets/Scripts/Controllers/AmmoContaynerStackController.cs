using Managers;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Controllers
{
    public class AmmoContaynerStackController :MonoBehaviour
    {
        [SerializeField]
        private AmmoContaynerManager _ammoContaynerManager;
        public int _currentCount;


        private List<GameObject> _ammoWorkerStackList=new List<GameObject>();

        private int Count;
        private float timer=0.1f;

        private async void Start()
        {
            _ammoContaynerManager.StackInfos(_currentCount, transform);

            await Task.Delay(10);

            _ammoContaynerManager.SendToTargetInfo(_ammoWorkerStackList);

        }
 

        public async void AddStack(List<Vector3> gridPosList)
        {

            if (Count < _ammoWorkerStackList.Count)
            {
                _ammoWorkerStackList[Count].transform.SetParent(transform);

             

                _ammoWorkerStackList[Count].transform.position = transform.position+gridPosList[Count];

                _ammoWorkerStackList[Count].transform.rotation = transform.rotation;

                Count++;


               
            }
            else
            {
                _ammoWorkerStackList.Clear();
                
                _ammoWorkerStackList.TrimExcess();
                //  _currentCount = _ammoWorkerStackList.Count;

                _ammoContaynerManager.StackInfos(_currentCount, transform);
                await Task.Delay(10);
                _ammoContaynerManager.SendToTargetInfo(_ammoWorkerStackList);
            }
        }
        
        public void RemoveStack()
        {

        }

        public void SetAmmoWorkerList(List<GameObject> ammoWorkerStackList)
        {
            Debug.Log("SetAmmoWorkerList");
            _ammoWorkerStackList = ammoWorkerStackList;
        }


    }
}