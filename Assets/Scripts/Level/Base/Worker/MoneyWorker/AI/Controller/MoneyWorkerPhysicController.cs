using UnityEngine;
using Signals;
using Interfaces;
using AIBrain.MoneyWorkers;

namespace Controllers
{
    public class MoneyWorkerPhysicController : MonoBehaviour
    {
        [SerializeField]
        private MoneyWorkerAIBrain moneyWorkerBrain;

        [SerializeField]
        private MoneyWorkerStackerController moneyStackerController;



        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<IStackable>(out IStackable stackable))
            {
                if (moneyWorkerBrain.IsAvailable())
                {
                    stackable.IsCollected = true;
                    MoneyWorkerSignals.Instance.onThisMoneyTaken?.Invoke();
                    moneyStackerController.SetStackHolder(stackable.SendToStack().transform);
                    moneyStackerController.GetStack(other.gameObject);
                    moneyWorkerBrain.SetCurrentStock();
                    //other'a layer deðiþtirme yapýlabilir
                }
            }


            if (other.TryGetComponent(out GatePhysicsController GatePhysicsController))
            {

                moneyStackerController.OnRemoveAllStack();
                moneyWorkerBrain.RemoveAllStock();
            }

        }
    } 
}
