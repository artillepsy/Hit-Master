using Enemy;
using Player;
using UnityEngine;

namespace Shooting
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private int damage = 50;
        [SerializeField] private float speed = 3f;
        [SerializeField] private float destroyDelaySec = 5f;
        private bool _collided = false;

        private void OnEnable()
        {
            _collided = false;
            rb.velocity = Vector3.zero;
            CancelInvoke(nameof(AddToPool));
            Invoke(nameof(AddToPool), destroyDelaySec);
        }

        private void FixedUpdate() => rb.velocity = transform.forward * speed;
        
        private void AddToPool() => BulletPool.Inst.Add(gameObject);

        private void OnCollisionEnter(Collision other)
        {
            if (_collided) return;
            if (other.collider.GetComponentInParent<PlayerShooting>()) return;
            if (other.collider.GetComponentInParent<Bullet>()) return;
            
            var comp = other.collider.GetComponentInParent<EnemyHealth>();
            if (comp) comp.TakeDamage(damage);
            _collided = true;
            BulletPool.Inst.Add(gameObject);
        }
    }
}