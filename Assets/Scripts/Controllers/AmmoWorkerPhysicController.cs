using Interfaces;
using Managers;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public  class AmmoWorkerPhysicController : MonoBehaviour
    {
        AmmoWorkerManager ammoWorkerManager;
        private float _timer=0;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(GetterConteynerInfo), out Component ammoManager))//it must change
            {
                ammoWorkerManager.IsHitAmmoWareHouse(other.gameObject.transform);//gecikme gerekebilir.

                ammoWorkerManager.IsSetConteynerLists(
                    other.GetComponent<AmmoManager>().GetterConteynerList(),
                    other.GetComponent<AmmoManager>().GetterConteynerMaxAmunt(),
                    other.GetComponent<AmmoManager>().GetterConteynerCurrentAmunt());

              
            }


        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.name == "wareHouse")//it must change
            {
                if (_timer<0.4f)
                {
                    ammoWorkerManager.IsStayWareHouse();
                    _timer = Time.deltaTime;
                }
                else
                {
                    _timer = 0;
                }
                
            }

           




        }
                



    }
}