using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class BulletPool : MonoBehaviour
    {
        public static BulletPool Inst { get; private set; }

        private List<GameObject> _bullets = new List<GameObject>();
        private void Awake() => Inst = this;

        public void Add(GameObject bullet)
        {
            bullet.SetActive(false);
            _bullets.Add(bullet);
        }

        public GameObject Get()
        {
            if (_bullets.Count == 0) return null;
            var bullet = _bullets[0];
            _bullets.Remove(bullet);
            return bullet;
        }
    }
}