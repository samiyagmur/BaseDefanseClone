using TMPro;
using UnityEngine;

namespace Controller
{
    public class RoomPaymentTextController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro remainingCostText;

        public void SetInitText(int cost) => remainingCostText.text = cost.ToString();

        public void UpdateText(int cost) => remainingCostText.text = cost.ToString();
    }
}