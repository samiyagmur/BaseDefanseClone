using Interfaces;
using Managers;
using Signals;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class ButtonPhsicsController : MonoBehaviour
    {
        private ButtonManager button;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(IGeterGameObject), out Component getterGameObject))
            {
                
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(typeof(IGeterGameObject), out Component getterGameObject))
            {
               // button.IsActiveByPlayer();
            }
        }
    }
}