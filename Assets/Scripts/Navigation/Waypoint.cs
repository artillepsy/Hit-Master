using UnityEngine;

namespace Navigation
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private int id;
        
        public int Id => id;
    }
}