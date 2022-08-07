using System.Collections;
using Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Image sliderImage;
        [Space]
        [SerializeField] private Gradient color;
        [Space]
        [SerializeField] private float updateSliderTimeSec = 0.2f;
        private Coroutine _updateHealthCO;
        public GameObject Enemy { private get; set; }

        private void Start()
        {
            slider.value = 1f;
            sliderImage.color = color.Evaluate(slider.value);
            EnemyHealth.OnEnemyHealthChange.AddListener(UpdateSlider);
        }

        private void UpdateSlider(GameObject enemy, float fillAmount)
        {
            if (enemy != Enemy) return;

            if (_updateHealthCO != null)
            {
                StopCoroutine(_updateHealthCO);
            }
            _updateHealthCO = StartCoroutine(UpdateHealthCO(fillAmount));
        }

        private IEnumerator UpdateHealthCO(float endValue)
        {
            var valueStep = (endValue - slider.value)/ updateSliderTimeSec;
            var time = 0f;
            while (time < updateSliderTimeSec)
            {
                slider.value += valueStep * Time.deltaTime;
                sliderImage.color = color.Evaluate(slider.value);
                time += Time.deltaTime;
                yield return null;
            }
            slider.value = endValue;
            sliderImage.color = color.Evaluate(slider.value);
            _updateHealthCO = null;
            
            if(endValue == 0f) gameObject.SetActive(false);
        }
    }
}