using Navigation;
using Shooting;
using UnityEngine;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Transform fireSource;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float planeDistance = 10f;
        private bool _shootingEnabled = false;
        private Camera _cam;
        
        private void Start()
        {
            _cam = Camera.main;
            PlayerMovement.OnPlayerRotate.AddListener(() => _shootingEnabled = true);
            Waypoint.OnClearWaypoint.AddListener(() => _shootingEnabled = false);
        }

        private void Update()
        {
            if (!_shootingEnabled) return;
            
            fireSource.forward = transform.forward;
            if (Input.touchCount == 0) return;
            if (Input.touches[0].phase != TouchPhase.Began) return;


            var ray = _cam.ScreenPointToRay(Input.touches[0].position);
            var plane = new Plane(-_cam.transform.forward, 
                transform.position + _cam.transform.forward * planeDistance);
            
            if (!plane.Raycast(ray, out var distance)) return;
            
            var endPoint = ray.GetPoint(distance);
            var inst = BulletPool.Inst.Get();

            if (!inst)
            {
                inst = Instantiate(bulletPrefab);
            }
            SetBulletData(inst, endPoint);
        }

        private void SetBulletData(GameObject inst, Vector3 endPoint)
        {
            inst.SetActive(true);
            inst.transform.position = fireSource.transform.position;
            inst.transform.forward = endPoint - fireSource.position;
        }
    }
}