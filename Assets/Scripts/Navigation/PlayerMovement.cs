using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Navigation
{
    public class PlayerMovement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private List<Waypoint> _waypoints;
        private Waypoint _currentWaypoint;
        private int _waypointIndex = 0;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _waypoints = FindObjectsOfType<Waypoint>().ToList();
            _waypoints = _waypoints.OrderBy(waypoint => waypoint.Id).ToList();
            _waypoints.ForEach(waypoint => Debug.Log(waypoint.Id));
            _currentWaypoint = _waypoints[0];
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            SetDestination();
        }

        private void SetDestination()
        {
            _agent.SetDestination(_currentWaypoint.transform.position);
            _waypointIndex++;
            
            StartCoroutine(RotateToTargetCO());

            if (_waypointIndex > _waypoints.Count - 1) return;
            _currentWaypoint = _waypoints[_waypointIndex];
        }

        private IEnumerator RotateToTargetCO()
        {
            var sqrStopDistance = _agent.stoppingDistance * _agent.stoppingDistance;
            var rotateRotation = _currentWaypoint.transform.rotation;
            while (true)
            {
                var distance = (transform.position - _agent.destination).sqrMagnitude;
                if (distance <= sqrStopDistance) break;
                yield return null;
            }

            while (Quaternion.Angle(transform.rotation, rotateRotation) > 0.5f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation,
                    rotateRotation,
                    Time.deltaTime * _agent.angularSpeed);
                yield return null;
            }
        }
    }
}
