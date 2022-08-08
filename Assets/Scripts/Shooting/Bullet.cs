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
        [Space]
        [SerializeField] private ParticleSystem hitPS;
        
        private TrailRenderer _trailRenderer;
        private bool _collided = false;

        private void Awake()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        private void OnEnable()
        {
            _collided = false;
            rb.velocity = Vector3.zero;
           
            CancelInvoke(nameof(AddToPool));
            Invoke(nameof(AddToPool), destroyDelaySec);
        }

        private void FixedUpdate() => rb.velocity = transform.forward * speed;
        
        private void AddToPool()
        {
            //_trailRenderer.Clear();
            BulletPool.Inst.Add(gameObject);
        }

        private void SpawnParticles()
        {
            var rotation = Quaternion.LookRotation(transform.position - transform.forward);
            var psInstance = Instantiate(hitPS, transform.position, rotation);
            psInstance.Play();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_collided) return;
            if (other.GetComponentInParent<PlayerShooting>()) return;
            if (other.GetComponentInParent<Bullet>()) return;
            
            var comp = other.GetComponentInParent<EnemyHealth>();
            if (comp) comp.TakeDamage(damage);
            _collided = true;
            SpawnParticles();
            BulletPool.Inst.Add(gameObject);
        }
    }
}