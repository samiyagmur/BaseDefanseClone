using System;
using Abstraction;
using Interfaces;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class DropZonePhysicController : MonoBehaviour
    {
        [SerializeField]
        private GemStackerController gemStackerController;
        [SerializeField] private Collider collider;
        private void OnTriggerEnter(Collider other)
        {
           
            //if (other.CompareTag("Gem"))
            //{
            //    Debug.Log("OnTriggerEnter");
            //    if (gemStackerController.PositionList.Count <= gemStackerController.StackList.Count)
            //    {
            //        return;
            //    }
              


            //    gemStackerController.GetStack(other.gameObject, other.transform);
            //}
            //else if (other.TryGetComponent<Interactable>(out Interactable interactable))
            //{
            //    gemStackerController.OnRemoveAllStack(other.transform);
            //}
        }
    }
}