using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int totalHealth = 100;
        [SerializeField] private int maxHealth = 100;
        
        public UnityEvent OnDie = new UnityEvent();

        public void TakeDamage(int damage)
        {
            totalHealth -= damage;
            if (totalHealth <= 0)
            {
                totalHealth = 0;
                OnDie?.Invoke();                
            }
            Debug.Log(totalHealth);
        }
        
        private void Awake() => totalHealth = maxHealth;
    }
}