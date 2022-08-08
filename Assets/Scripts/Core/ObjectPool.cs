using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private List<GameObject> prefabs;
        
        public static ObjectPool Inst { get; private set; }

        private List<GameObject> _objects = new List<GameObject>();
        private void Awake() => Inst = this;

        public void Add(GameObject go)
        {
            go.SetActive(false);
            _objects.Add(go);
        }
        
        public void Spawn(string objetsTag, Vector3 pos, Quaternion rotation)
        {
            var inst = Get(objetsTag);
            if (!inst)
            {
                Instantiate(GetPrefab(objetsTag), pos, rotation);
                return;
            }
            inst.transform.position = pos;
            inst.transform.rotation = rotation;
            inst.SetActive(true);
        }

        private GameObject Get(string objetsTag)
        {
            if (_objects.Count == 0) return null;
            GameObject go = null;
            foreach (var current in _objects)
            {
                if(!current.CompareTag(objetsTag)) continue;
                go = current;
            }
            if(go) _objects.Remove(go);
            return go;
        }

        private GameObject GetPrefab(string prefabTag)
        {
            foreach (var prefab in prefabs)
            {
                if(!prefab.CompareTag(prefabTag)) continue;
                return prefab;
            }
            Debug.LogError("Required prefab not found");
            return null;
        }
    }
}