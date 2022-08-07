using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace Navigation
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private bool isFinish = false;
        [SerializeField] private bool removeMeshes = true;
        [Space]
        [SerializeField] private List<EnemyHealth> enemyHealthComponents;
        private int _needKillCount;
        private int _killedEnemies = 0;

        public static UnityEvent OnClearWaypoint = new UnityEvent();
        
        public int Id => id;
        public bool IsFinish => isFinish;

        private void Awake()
        {
            if (!removeMeshes) return;

            foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())          
            {
                meshFilter.gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            _needKillCount = enemyHealthComponents.Count;
            foreach (var component in enemyHealthComponents)
            {
                component.OnDie.AddListener(IncrementKillCount);
            }
        }

        private void IncrementKillCount()
        {
            _killedEnemies++;
            if (_killedEnemies < _needKillCount) return;
            OnClearWaypoint?.Invoke();
        }
    }
}