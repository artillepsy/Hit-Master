using UnityEngine;

namespace Core
{
    public class AudioSourceDeactivator : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSrc;
        [SerializeField] private AudioClip audioClip;

        private void OnEnable()
        {
            audioSrc.PlayOneShot(audioClip);
            Invoke(nameof(AddToPool), audioClip.length);
        }

        private void AddToPool() => ObjectPool.Inst.Add(gameObject);
    }
}