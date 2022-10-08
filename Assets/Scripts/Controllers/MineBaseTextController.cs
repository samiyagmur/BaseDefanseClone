using System.Collections;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class MineBaseTextController : MonoBehaviour
    {
        public void UpdateMineWorkerAmountText(TextMeshPro gemWorkerText, int currentWorkerAmount, int maxWorkerAmount)
        {
            gemWorkerText.text = currentWorkerAmount + "/" + maxWorkerAmount + " \n Gem Mine";
        }

    }
}