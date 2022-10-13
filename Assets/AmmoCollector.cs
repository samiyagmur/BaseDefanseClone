using Concreate;
using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class AmmoCollector : MonoBehaviour
    {   
        [SerializeField] 
        private AmmoStackerController ammoStackerController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out StackableAmmo stackableAmmo))
            {
 
                if (ammoStackerController.PositionList.Count <= ammoStackerController.StackList.Count)
                {
                    return;
                }
                ammoStackerController.SetStackHolder(stackableAmmo.SendToStack().transform);
                ammoStackerController.GetStack(other.gameObject);
            }

            if (other.TryGetComponent(out AmmoDropZonePhysicsController ammoDropZonePhysicsController))//it must change
            {
                //_ammoManager.WhenEnterTurretStack(transform.parent.GetComponent<AmmoWorkerBrain>());

                ammoStackerController.OnRemoveAllStack();
            }

            else if (other.CompareTag("GateEnter"))
            {
                ammoStackerController.OnRemoveAllStack();
            }

        }
    } 
}
