using Enemy;
using UnityEngine;

namespace Shooting
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private int damage = 50;
        [SerializeField] private float speed = 3f;
        [SerializeField] private float destroyDelaySec = 5f;

        private void Start() => Invoke(nameof(AddToPool), destroyDelaySec);

        private void FixedUpdate() => rb.velocity = transform.forward * speed;
        
        private void AddToPool() => BulletPool.Inst.Add(gameObject);

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<PlayerShooting>()) return;
            if (other.GetComponentInParent<Bullet>()) return;
            
            var comp = GetComponentInParent<EnemyHealth>();

            if (comp) comp.TakeDamage(damage);   
            
            BulletPool.Inst.Add(gameObject);
        }
    }
}