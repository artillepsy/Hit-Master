using UnityEngine;

namespace Shooting
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Transform fireSource;
        [SerializeField] private GameObject bulletPrefab;
        private Camera _cam;

        private void Start() => _cam = Camera.main;

        private void Update()
        {
            if (Input.touchCount == 0) return;
            if (Input.touches[0].phase != TouchPhase.Began) return;

            /*var ray = _cam.ScreenPointToRay(Input.touches[0].position);
            var hit = 

            var inst = BulletPool.Inst.Get();
            
            if (!inst) Instantiate(bulletPrefab, fireSource.position, transform.rotation);
            else SetBulletData(inst);*/
        }

        private void SetBulletData(GameObject inst)
        {
            inst.SetActive(true);
            inst.transform.position = fireSource.transform.position;
            inst.transform.forward = transform.forward;
        }
    }
}