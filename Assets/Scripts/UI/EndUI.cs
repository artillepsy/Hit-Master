using Player;
using UnityEngine;

namespace UI
{
    public class EndUI : MonoBehaviour
    {
        [SerializeField] private Animation startCanvasAnim;

        private void Awake()
        {
            PlayerMovement.OnFinishReach.AddListener(() => );
        }
    }
}