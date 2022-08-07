using UnityEngine;
using UnityEngine.Events;

namespace Level
{
    public class StartUI : MonoBehaviour
    {
        [SerializeField] private GameObject startText;
        private bool _started = false;
        public static UnityEvent OnLevelStart = new UnityEvent();

        private void Update()
        {
            if (_started) return;
            if (Input.touchCount == 0) return;
            
            startText.SetActive(false); // play animation
            OnLevelStart?.Invoke();
            
            _started = true;
            enabled = false;
        }
    }
}