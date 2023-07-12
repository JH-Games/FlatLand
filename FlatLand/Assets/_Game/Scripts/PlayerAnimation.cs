using UnityEngine;

namespace _Game.Scripts {
    public static class PlayerAnimation {
        private static Animator _animator;
        private static readonly int _speed = Animator.StringToHash("Speed");
        private static readonly int _motionSpeed = Animator.StringToHash("MotionSpeed");

        private static readonly int _isSneak = Animator.StringToHash("Sneak");
        private static readonly int _isAim = Animator.StringToHash("Aim");
        private static readonly int _isUnsheathed = Animator.StringToHash("Unsheathed");

        private static readonly int _isGrounded = Animator.StringToHash("Grounded");
        private static readonly int _isJump = Animator.StringToHash("Jump");
        private static readonly int _IsFreeFall = Animator.StringToHash("FreeFall");
        
        private static readonly int _aimMoveSideways = Animator.StringToHash("Aim_Move_Sideways");
        private static readonly int _aimMoveForward = Animator.StringToHash("Aim_Move_Forward");

        
           
        public static bool FreeFall {
            set => _animator.SetBool(_IsFreeFall, value);
        }

        public static bool Grounded {
            set => _animator.SetBool(_isGrounded, value);
        }

        public static bool Jump {
            set => _animator.SetBool(_isJump, value);
        }

        public static bool Unsheathed {
            get => _animator.GetBool(_isUnsheathed);
            set => _animator.SetBool(_isUnsheathed, value);
        }


        public static void Init(Animator animator) {
            _animator = animator;
            PlayerStates.OnPlayerStateChanged += HandlePlayerStateChanged;
        }
        
       

        private static void HandlePlayerStateChanged(PlayerState newState) {
            _animator.SetBool(_isSneak, newState == PlayerState.Sneak);
            _animator.SetBool(_isAim, newState == PlayerState.Aim);
        }

        public static void SetSpeed(float speed) => _animator.SetFloat(_speed, speed);
        public static void SetMotionSpeed(float speed) => _animator.SetFloat(_motionSpeed, speed);
        public static void SetDirection(Vector2 inputMove) {
            _animator.SetFloat(_aimMoveSideways, inputMove.x);
            _animator.SetFloat(_aimMoveForward, inputMove.y);
        }

        public static void Dispose() {
            PlayerStates.OnPlayerStateChanged -= HandlePlayerStateChanged;
        }


       
    }
}