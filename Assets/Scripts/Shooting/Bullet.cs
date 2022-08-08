using Core;
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
        
        private void AddToPool() => ObjectPool.Inst.Add(gameObject);

        private void SpawnEffects()
        {
            var rotation = Quaternion.LookRotation(transform.position - transform.forward);
            ObjectPool.Inst.Spawn(Literals.Tags.HitPS, transform.position, rotation);
            ObjectPool.Inst.Spawn(Literals.Tags.HitAudioSrc, transform.position, rotation);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_collided) return;
            if (other.GetComponentInParent<PlayerShooting>()) return;
            if (other.GetComponentInParent<Bullet>()) return;
            
            var comp = other.GetComponentInParent<EnemyHealth>();
            if (comp) comp.TakeDamage(damage);
            _collided = true;
            SpawnEffects();
            ObjectPool.Inst.Add(gameObject);
        }
    }
}