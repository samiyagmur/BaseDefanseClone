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

        private List<GameObject> _ammoWorkerStackList=new List<GameObject>();
       
        public int _currentCount;

        private async void Start()
        {
            
            _ammoContaynerManager.StackInfos(_currentCount, transform);

            await Task.Delay(50);

           _ammoContaynerManager.SendToTargetInfo(_ammoWorkerStackList);
          
        }
 

        public async void AddStack(List<Vector3> gridPosList)
        {

            

            if (_currentCount < gridPosList.Count)
            {
                if (_currentCount < _ammoWorkerStackList.Count)
                {

                    _ammoWorkerStackList[_currentCount].transform.SetParent(transform);

                    _ammoWorkerStackList[_currentCount].transform.position = transform.position + gridPosList[_currentCount];

                    _ammoWorkerStackList[_currentCount].transform.rotation = transform.rotation;

                    _currentCount++;
                }
                else
                {
                    
                    _ammoWorkerStackList.Clear();

                    _ammoWorkerStackList.TrimExcess();

                    _ammoContaynerManager.StackInfos(_currentCount, transform);

                    Debug.Log(_currentCount + " " + transform);

                    await Task.Delay(10);

                    _ammoContaynerManager.SendToTargetInfo(_ammoWorkerStackList);
                }
            }
            
        }
        
        public void RemoveStack()
        {

        }

        public void SetAmmoWorkerList(List<GameObject> ammoWorkerStackList)
        {
            Debug.Log(ammoWorkerStackList.Count);
            _ammoWorkerStackList = ammoWorkerStackList;
        }


    }
}