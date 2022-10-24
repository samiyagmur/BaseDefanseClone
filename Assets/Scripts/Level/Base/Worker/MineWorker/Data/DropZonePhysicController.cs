using Abstraction;
using Concreate;
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
            if (other.TryGetComponent(out StackableGem stackableGem))
            {
                if (gemStackerController.PositionList.Count <= gemStackerController.StackList.Count)
                {
                    return;
                }

                gemStackerController.GetStack(other.gameObject, other.transform);
            }
            else if (other.TryGetComponent<PlayerInteractable>(out PlayerInteractable interactable))
            {
       
                gemStackerController.OnRemoveAllStack(other.transform.parent.gameObject.transform);
            }
        }
    }
}