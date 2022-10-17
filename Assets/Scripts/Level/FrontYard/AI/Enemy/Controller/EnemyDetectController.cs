using UnityEngine;
using Managers;
using AIBrain;
using Abstraction;
using Interfaces;
using AIBrain.EnemyBrain;

namespace Controllers
{   
    public class EnemyDetectController : MonoBehaviour
    {
        [SerializeField]
        private EnemyBrain brain;

        private Transform _detectPlayer;
        private bool _amIDead;
        private Transform _detectedMine;

        public bool IsBombInRange() => _detectedMine != null;

        public bool AmIDead { get => _amIDead; set => _amIDead = value; }

        private void OnTriggerEnter(Collider other) 
        {

            if (other.TryGetComponent(typeof(PlayerManager), out Component getterTurretObject)) brain.PlayerTarget = other.transform;


            //if (other.CompareTag("LandMine"))
            //{
            //    _detectedMine = other.transform;
            //    brain.MineTarget = _detectedMine;


            //}

            //if (other.CompareTag("MineExplosion"))
            //{
            //    Debug.Log("MineExplosion");
            //    int damageAmount = other.transform.parent.parent.GetComponent<IDamager>().GetDamage();
            //    Debug.Log(damageAmount);
            //    brain._health -= damageAmount;
            //    if (brain._health <= 0)
            //    {
            //        _amIDead = true;

            //    }
            //}

            if (other.TryGetComponent(typeof(IDamager), out Component takeDamage))
            {
                
            }
           
        }
        private void OnTriggerExit(Collider other) 
        {
            if (other.TryGetComponent(typeof(PlayerManager), out Component getterTurretObject)) brain.PlayerTarget = null;


            //if (other.CompareTag("LandMine"))
            //{
            //    _detectPlayer = null;
            //   brain.MineTarget = _detectedMine;
            //   brain.MineTarget = null;
            //}

            //if (other.CompareTag("MineExplosion"))
            //{
            //    _detectPlayer = null;
            //    brain.MineTarget = _detectedMine;
            //    brain.MineTarget = null;
            //}


        }

    
    } 
}