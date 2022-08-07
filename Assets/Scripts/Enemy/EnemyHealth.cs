using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int totalHealth = 100;
        [SerializeField] private int maxHealth = 100;
        
        private bool _dead = false;
        public UnityEvent OnDie = new UnityEvent();

        public void TakeDamage(int damage)
        {
            if (_dead) return;
            totalHealth -= damage;
            if (totalHealth <= 0)
            {
                _dead = true;
                totalHealth = 0;
                OnDie?.Invoke();
            }
            Debug.Log(totalHealth);
        }
        
        private void Awake() => totalHealth = maxHealth;
    }
}