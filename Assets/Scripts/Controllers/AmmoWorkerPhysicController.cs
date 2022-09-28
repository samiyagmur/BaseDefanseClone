using Interfaces;
using Managers;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Controllers
{
    public  class AmmoWorkerPhysicController : MonoBehaviour
    {
        [SerializeField]
        private AmmoWorkerManager ammoWorkerManager;
        private float _timer=0;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(GetterConteynerInfo), out Component ammoManager))//it must change
            {
                Delay(other);
            }
        }

        private async void Delay(Collider other)
        {   
            
            await Task.Delay(50);

            ammoWorkerManager.IsSetConteynerLists(
                other.GetComponent<AmmoManager>().GetterConteynerList(),
                other.GetComponent<AmmoManager>().GetterConteynerMaxAmunt(),
                other.GetComponent<AmmoManager>().GetterConteynerCurrentAmunt());
            await Task.Delay(500);
            ammoWorkerManager.IsEnterAmmoWareHouse();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(typeof(GetterConteynerInfo), out Component ammoManager))//it must change
            {
                ammoWorkerManager.IsExitAmmoWareHouse();//gecikme gerekebilir.

            }
        }

        private void OnTriggerStay(Collider other)
        {
            ammoWorkerManager.IsStayWareHouse();
        }




    }
}