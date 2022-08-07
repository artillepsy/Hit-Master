using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Navigation;
using UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform body;
        private NavMeshAgent _agent;
        private List<Waypoint> _waypoints;
        private Waypoint _nextWaypoint;
        private int _waypointIndex = 0;
        
        public static UnityEvent<Waypoint> OnWaypointUpdate = new UnityEvent<Waypoint>();
        public static UnityEvent OnPlayerStop = new UnityEvent();
        public static UnityEvent OnPlayerRotate = new UnityEvent();
        public static UnityEvent OnPlayerStartMove = new UnityEvent();
        public static UnityEvent OnFinishReach = new UnityEvent();

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            
            _waypoints = FindObjectsOfType<Waypoint>().ToList();
            _waypoints = _waypoints.OrderBy(waypoint => waypoint.Id).ToList();
            _waypoints.ForEach(waypoint => Debug.Log(waypoint.Id));
            
            StartUI.OnLevelStart.AddListener(SetDestination);
            Waypoint.OnClearWaypoint.AddListener(SetDestination);
        }

        private void SetDestination()
        {
            if (_waypointIndex > _waypoints.Count - 1) return;
            
            _nextWaypoint = _waypoints[_waypointIndex];
            _nextWaypoint.ActivateEnemies();
            
            var destination = _nextWaypoint.transform.position;
            destination.y = transform.position.y;
            
            _agent.SetDestination(destination);
            _waypointIndex++;
            OnPlayerStartMove?.Invoke();
            StartCoroutine(RotateToTargetCO());
        }

        private IEnumerator RotateToTargetCO()
        {
            var sqrStopDistance = _agent.stoppingDistance * _agent.stoppingDistance;
            var targetRotation = Quaternion.Euler(0, _nextWaypoint.transform.rotation.eulerAngles.y, 0);
            
            while (true)
            {
                var distance = (transform.position - _agent.destination).sqrMagnitude;
                if (distance <= sqrStopDistance) break;
                yield return null;
            }
            OnPlayerStop?.Invoke();
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.5f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation,
                    targetRotation,
                    Time.deltaTime * _agent.angularSpeed);
                yield return null;
            }
            OnWaypointUpdate?.Invoke(_nextWaypoint);
            if(_nextWaypoint.IsFinish) OnFinishReach?.Invoke();
            else OnPlayerRotate?.Invoke();
        }
    }
}
