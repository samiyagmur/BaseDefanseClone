using Signals;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {

        private int totalGemValue;
        private int totalMoneyValue;
        [SerializeField]
        private TextMeshProUGUI gemText;
        [SerializeField]
        private TextMeshProUGUI moneyText;

       
        #region EventSubscription
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onUpdateMoneyScore += OnUpdateMoneyScore;
            CoreGameSignals.Instance.onUpdateGemScore += OnUpdateGemScore;

        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onUpdateMoneyScore -= OnUpdateMoneyScore;
            CoreGameSignals.Instance.onUpdateGemScore -= OnUpdateGemScore;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void OnUpdateGemScore(int gemValue)
        {
            totalGemValue += gemValue;
            gemText.text = totalGemValue.ToString();

        }

        private void OnUpdateMoneyScore(int moneyValue)
        {
            totalMoneyValue += moneyValue;
            moneyText.text = totalMoneyValue.ToString();
        }


        #endregion




    }
}