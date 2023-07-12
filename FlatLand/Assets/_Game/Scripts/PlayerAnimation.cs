using System;
using OknaaEXTENSIONS.CustomWrappers;
using UnityEngine;

namespace _Game.Scripts {
    public static class PlayerAnimation {
        private static Animator _animator;
        private static readonly int _animIDSpeed = Animator.StringToHash("Speed");
        private static readonly int _animIDSneak = Animator.StringToHash("Sneak");
        private static readonly int _animIDGrounded = Animator.StringToHash("Grounded");
        private static readonly int _animIDJump = Animator.StringToHash("Jump");
        private static readonly int _animIDFreeFall = Animator.StringToHash("FreeFall");
        private static readonly int _animIDUnsheathed = Animator.StringToHash("Unsheathed");
        private static readonly int _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        
           
        public static bool FreeFall {
            get => _animator.GetBool(_animIDFreeFall);
            set => _animator.SetBool(_animIDFreeFall, value);
        }

        public static bool Grounded {
            get => _animator.GetBool(_animIDGrounded);
            set => _animator.SetBool(_animIDGrounded, value);
        }

        public static bool Jump {
            get => _animator.GetBool(_animIDJump);
            set => _animator.SetBool(_animIDJump, value);
        }

        public static bool Unsheathed {
            get => _animator.GetBool(_animIDUnsheathed);
            set => _animator.SetBool(_animIDUnsheathed, value);
        }


        public static void Init(Animator animator) {
            _animator = animator;
            PlayerStates.OnPlayerStateChanged += HandlePlayerStateChanged;
        }
        
       

        private static void HandlePlayerStateChanged(PlayerState newState) => _animator.SetBool(_animIDSneak, newState == PlayerState.Sneak);

        public static void SetSpeed(float speed) => _animator.SetFloat(_animIDSpeed, speed);
        public static void SetMotionSpeed(float speed) => _animator.SetFloat(_animIDMotionSpeed, speed);


        public static void Dispose() {
            PlayerStates.OnPlayerStateChanged -= HandlePlayerStateChanged;
        }
        
       
    }
}