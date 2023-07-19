using System;
using OknaaEXTENSIONS.CustomWrappers;
using Player.Animation;
using Player.StateMachines.MoveStates;
using Player.StateMachines.WeaponStates;
using Systems;
using UnityEngine;

namespace Player.StateMachines.Base {
    public class PlayerStateMachine : Singleton<PlayerStateMachine> {
        public static Action<State> OnMoveStateChanged;
        public GameObject CameraTarget;
        public PlayerData playerData;

        public float Speed { get; private set; }
        public static MoveState CurrentMoveState => _currentMoveState;
        private static MoveState _currentMoveState;

        public static WeaponState CurrentWeaponState => _currentWeaponState;
        private static WeaponState _currentWeaponState;
        
        public static CharacterController CharacterController => _characterController;
        private static CharacterController _characterController;
        

        public void Init() {
            PlayerAnimation.Init(GetComponent<Animator>());
            _characterController = GetComponent<CharacterController>();

            SwitchMoveState(new NormalState());
            SwitchWeaponState(new SheathedState());
        }


        private void Update() {
            _currentMoveState?.Tick(transform, Time.deltaTime);
            _currentWeaponState?.Tick(transform, Time.deltaTime);
        }

        public static void SwitchMoveState(MoveState state) {
            if (_currentMoveState == state) return;
            _currentMoveState?.Exit();
            _currentMoveState = state;
            _currentMoveState?.Enter();
            OnMoveStateChanged?.Invoke(state);
        }

        public static void SwitchWeaponState(WeaponState state) {
            if (_currentWeaponState == state) return;
            _currentWeaponState?.Exit();
            _currentWeaponState = state;
            _currentWeaponState?.Enter();
        }

        public void UpdateSpeed(Vector2 move) {
            if (move == Vector2.zero) {
                Speed = 0;
                return;
            }

            if (_currentMoveState is ParkourState) {
                Speed = playerData.RunSpeed;
            }
            else if (_currentMoveState is SneakState) {
                Speed = playerData.SneakSpeed;
            }
            else if (_currentMoveState is NormalState) {
                Speed = playerData.WalkSpeed;
            }
        }
        
        
        private void OnFootstep(AnimationEvent animationEvent) => SoundSystem.Instance.PlayFootStep(animationEvent, transform.TransformPoint(_characterController.center));
        private void OnLand(AnimationEvent animationEvent) => SoundSystem.Instance.PlayLand(animationEvent, transform.TransformPoint(_characterController.center));
    }
}