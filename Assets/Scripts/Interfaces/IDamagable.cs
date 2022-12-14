using UnityEngine;

namespace Interfaces
{
    public interface IDamagable
    {
        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }

        int TakeDamage(int damage);

        Transform GetTransform();
    }
}