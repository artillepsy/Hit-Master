using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyAnimationOffsetSetter : MonoBehaviour
    {
        private static readonly int Offset = Animator.StringToHash("Offset");

        private void Start()
        {
            GetComponentInChildren<Animator>().SetFloat(Offset, Random.Range(0f, 1f));
        }
    }
}