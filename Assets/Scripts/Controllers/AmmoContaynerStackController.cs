using Enums;
using Interfaces;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class AmmoContaynerStackController : MonoBehaviour
    {
        [SerializeField]
        private AmmoContaynerManager ammoContaynerManager;
        private List<GameObject> _contaynerStackList;
        public int testNumber;
        public bool _IsFull;
        private void Awake()
        {
          
            AddStack();
        }
        private void Start()
        {
            ammoContaynerManager.InvokeRepeating("SendToTargetInfo", 1f, 5f);
        }
        public void AddStack()
        {

            if (testNumber <= 50)
            {
                ammoContaynerManager.StackCount(testNumber, gameObject.transform.parent.gameObject);
            }


           // _contaynerStackList.Add(stackObject);

            

        }
        
        public void RemoveStack()
        {

        }



   
       
    }
}