using Navigation;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Walking = Animator.StringToHash("Walking");

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            PlayerMovement.OnPlayerStartMove.AddListener(() => SetWalkState(true));
            PlayerMovement.OnPlayerStop.AddListener(() => SetWalkState(false));
        }

        private void SetWalkState(bool isWalking)
        {
            _animator.SetBool(Walking, isWalking);
        }
    }
}