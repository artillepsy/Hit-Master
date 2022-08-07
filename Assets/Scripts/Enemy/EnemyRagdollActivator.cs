using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy
{
    public class EnemyRagdollActivator : MonoBehaviour
    {
        private Animator _animator;
        private List<Rigidbody> _rigidbodies;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _rigidbodies = GetComponentsInChildren<Rigidbody>().ToList();
            GetComponent<EnemyHealth>().OnDie.AddListener(ActivateRagdoll);
        }

        private void ActivateRagdoll()
        {
            foreach (var rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = false;
            }
            _animator.enabled = false;
        }
        
    }
}