using UnityEngine;

namespace Core
{
    public class ParticlesDeactivator : MonoBehaviour
    {
        [SerializeField] private ParticleSystem ps;

        private void OnDisable()  => ObjectPool.Inst.Add(gameObject);

        private void OnEnable() => ps.Play();
    }
}