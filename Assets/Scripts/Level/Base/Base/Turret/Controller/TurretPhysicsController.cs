//using AIBrain;
//using Assinger;
//using Enums;
//using Interfaces;
//using Managers;
//using System.Collections;
//using UnityEngine;

//namespace Controllers
//{
//    public class TurretPhysicsController : MonoBehaviour
//    {
        
//        [SerializeField]
//        private TurretManager _turretManager;
//        [SerializeField]
//        private TurretKeyAssinger turretKeyAssinger;


//        private void OnTriggerEnter(Collider other)
//        {
//            if (other.TryGetComponent(out PlayerPhysicsController getterGameObject))
//            {
//                _turretManager.IsEnterUser(turretKeyAssinger.TurretKey);
//            }

//        }

//        private void OnTriggerExit(Collider other)
//        {
//            if (other.TryGetComponent(out PlayerPhysicsController getterGameObject))
//            {
//                _turretManager.IsExitUser(turretKeyAssinger.TurretKey);
//            }
//        }


//    }
//}