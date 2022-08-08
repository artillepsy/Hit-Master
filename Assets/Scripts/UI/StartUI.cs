using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class StartUI : MonoBehaviour
    {
        [SerializeField] private Animation startCanvasAnim;
        [SerializeField] private AnimationClip disappearClip;
        
        private bool _started = false;
        public static UnityEvent OnLevelStart = new UnityEvent();

        private void DisableStartCanvas()
        {
            startCanvasAnim.gameObject.SetActive(false);
            enabled = false;
        }

        private void Update()
        {
            if (_started) return;
            if (Input.touchCount == 0) return;
            
            startCanvasAnim.Play(disappearClip.name);
            OnLevelStart?.Invoke();
            Invoke(nameof(DisableStartCanvas), disappearClip.length);
            _started = true;
        }
    }
}