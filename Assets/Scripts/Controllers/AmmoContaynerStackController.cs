using Enums;
using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class AmmoContaynerStackController : MonoBehaviour
    {

        private List<GameObject> _contaynerStackList;
        public float testNumber;
        public bool _IsFull;

       

        public void AddStack(Vector3 stackPositon, GameObject stackObject, float maxAmount)
        {

            if (_contaynerStackList.Count == maxAmount) _IsFull = true;
            else _IsFull = false;


            //dolcak
            _contaynerStackList.Add(stackObject);


        }

        public void RemoveStack()
        {
           //dolcak
        }


        public float CurrentAmunt()
        {

            return testNumber;//_contaynerStackList.Count;
        }

   
       
    }
}