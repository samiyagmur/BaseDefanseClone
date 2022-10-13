using AIBrain;
using Managers;
using System.Collections;
using UnityEngine;
using Interfaces;
using Abstraction;
using Concreate;

namespace Controllers
{
    public class AmmoDropZonePhysicsController : MonoBehaviour
    {
        [SerializeField]
        private AmmoDropZoneStacker ammoDropZoneStacker; 
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<StackableAmmo>(out StackableAmmo interactable))
            {
               
                if (ammoDropZoneStacker.PositionList.Count <= ammoDropZoneStacker.StackList.Count)
                {
                    Debug.Log(ammoDropZoneStacker.PositionList.Count + "AmmoDropZonePhysicsController" + ammoDropZoneStacker.StackList.Count);
                    return;
                }
             
                ammoDropZoneStacker.GetStack(interactable.SendToStack(), interactable.SendToStack().transform);
            }
            //else if (other.TryGetComponent(out AmmoWorkerInteractable ammoWorkerInteractable))
            //{
            //    Debug.Log("other.transfrom" + other.transform.name + " " + other.transform.parent.name);
            //    ammoDropZoneStacker.OnRemoveAllStack(other.transform);
            //}

        }
  
    }
}