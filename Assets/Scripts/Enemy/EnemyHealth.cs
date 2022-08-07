using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int totalHealth = 100;
        [SerializeField] private int maxHealth = 100;
        private bool _isDamageable = false;
        private bool _dead = false;
        
        public UnityEvent OnDie = new UnityEvent();
        public static UnityEvent<GameObject, float> OnEnemyHealthChange = new UnityEvent<GameObject, float>();
        public static UnityEvent<GameObject> OnCanTakeDamage = new UnityEvent<GameObject>();
        
        public void SetDamageable()
        {
            _isDamageable = true;
            OnCanTakeDamage?.Invoke(gameObject);           
        }
        
        public void TakeDamage(int damage)
        {
            if (!_isDamageable) return;
            if (_dead) return;
            totalHealth -= damage;
            if (totalHealth <= 0)
            {
                _dead = true;
                totalHealth = 0;
                OnDie?.Invoke();
            }
            OnEnemyHealthChange?.Invoke(gameObject, (float)totalHealth / maxHealth);
            Debug.Log(totalHealth);
        }
        
        private void Awake() => totalHealth = maxHealth;
    }
}