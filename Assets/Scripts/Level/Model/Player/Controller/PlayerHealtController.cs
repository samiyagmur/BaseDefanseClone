using Datas.ValueObject;
using Enums;
using Managers;
using Signals;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Data.ValueObject;

namespace Controller
{
    public class PlayerHealtController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField]
        private PlayerManager playerManager;

        [SerializeField]
        private TextMeshProUGUI healthText;
        [SerializeField]
        private Image healtBar;

        #endregion

        #region Private Variables

        private PlayerCharacterData _data;

        private int _health;

        private const int _increaseAmount = 1;

      

        #endregion

        #endregion
        
        public void SetHealthData(PlayerCharacterData data)
        {
            _data = data;
            Init();
        }

        private void Init()
        {
            _health = _data.PlayerHealth;
        }

        public async void IncreaseHealth()
        {

            if (playerManager.CurrentAreaType != AreaType.BaseDefense)
                return;

            if (_health == _data.MaxHealt)
            {
                playerManager.CloseHealtBar();
                
                return;
            }
            _health += _increaseAmount;
            OnHealthUpdate(_health);

            await Task.Delay(50);
            IncreaseHealth();

        }
        public void OnTakeDamage(int damage)
        {
            if (_health>=0)
                _health -= damage; 
            else
            {
                playerManager.ResetPlayer();
                _health = 100;
            }
                

            OnHealthUpdate(_health);
            OnSetHealthText(_health);
            if (_health != 0) return;
            playerManager.CloseHealtBar();
        }

        private void OnSetHealthText(float healthValue) => healthText.text = healthValue+"%"+_data.MaxHealt;
        private void OnHealthUpdate(int healthValue) 
        {

            healtBar.fillAmount= (float)healthValue / _data.MaxHealt;
         
            if (healthValue == _data.MaxHealt)
            {   

                playerManager.CloseHealtBar();
            }
        }
    }
}