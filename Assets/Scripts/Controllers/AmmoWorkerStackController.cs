using Abstraction;
using Enums;
using Interfaces;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class AmmoWorkerStackController: IStackable
    {

        private StackStatus _stackStatus;
    
        public void AddStack(Vector3 stackPositon, GameObject stackObject,Transform startPoint)
        {
            if (_stackStatus == StackStatus.Start)
            {
                //dolacak
                //Instantiate(stackObject, stackPositon, Quaternion.identity);
            }
        }

        public void RemoveStack()
        {
            throw new System.NotImplementedException();
        }

        public void StartStack(StackStatus stackStatus)
        {
            _stackStatus = stackStatus;
        }

        public void StopStack(StackStatus stackStatus)
        {
            _stackStatus = stackStatus;
        }

        
    }
}