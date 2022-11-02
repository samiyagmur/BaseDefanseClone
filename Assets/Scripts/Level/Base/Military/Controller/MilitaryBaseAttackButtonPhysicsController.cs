using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class MilitaryBaseAttackButtonPhysicsController : MonoBehaviour
    {
        [SerializeField] private MilitaryBaseManager manager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                AISignals.Instance.onSoldierActivation?.Invoke();
            }
        }
    }
}