using Interfaces;
using StateMachines.AIBrain.Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class BossHealtController : MonoBehaviour,IDamagable
    {

        [SerializeField] 
        private Image health;

        [SerializeField]
        private TextMeshProUGUI healthText;

        [SerializeField] 
        private int maxHealth;

        [SerializeField] 
        private BossEnemyBrain bossEnemyBrain;
        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }
        public int TakeDamage(int damage)
        {
           

            var currentHealth = bossEnemyBrain.Health -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                IsDead = true;
                UpdateHealth(currentHealth);
                return currentHealth;
            }
            UpdateHealth(currentHealth);
            return currentHealth;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void SetHealth(int initHealth)
        {
            maxHealth = initHealth;
            healthText.text = maxHealth.ToString();
        }
        private void UpdateHealth(int _currentHealth)
        {
            health.fillAmount = (_currentHealth / (float)maxHealth);
            healthText.text = _currentHealth.ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (!other.TryGetComponent(out IAttacker attacker)) return;

            TakeDamage(attacker.Damage());
        }
    }
}