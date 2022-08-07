using System.Collections.Generic;
using System.Linq;
using Enemy;
using Navigation;
using Player;
using UnityEngine;

namespace UI
{
    public class HealthBarsCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private GameObject healthBarPrefab;
        [SerializeField] private float barOffsetY = 1.2f;
        private Dictionary<EnemyHealth, HealthBar> _enemies = new Dictionary<EnemyHealth, HealthBar>();
        private Camera _cam;
        public static HealthBarsCanvas Inst { get; private set; }

        private void Awake() => Inst = this;

        private void Start()
        {
            _cam = Camera.main;
            SpawnHealthBars();
            PlayerMovement.OnWaypointUpdate.AddListener(ActivateHealthBars);   
        }
        
        private void Update()
        {
            foreach (var pair in _enemies)
            {
                if (!pair.Value) continue;
                
                pair.Value.transform.position = _cam.WorldToScreenPoint(
                    pair.Key.transform.position + Vector3.up * barOffsetY);
            }
        }

        private void ActivateHealthBars(Waypoint waypoint)
        {
            foreach (var healthComponent in waypoint.EnemyHealthComponents)
            {
                _enemies[healthComponent].gameObject.SetActive(true);
            }
        }
        private void SpawnHealthBars()
        {
            var enemies = FindObjectsOfType<EnemyHealth>().ToList();
            
            foreach (var enemy in enemies)
            {
                var inst  = Instantiate(healthBarPrefab, canvas.transform);
                
                inst.transform.position =_cam.WorldToScreenPoint(
                    enemy.transform.position + Vector3.up * barOffsetY);
                
                var bar = inst.GetComponent<HealthBar>();
                bar.Enemy = enemy.gameObject;
                _enemies.Add(enemy, bar);
                inst.SetActive(false);
            }
        }
    }
}