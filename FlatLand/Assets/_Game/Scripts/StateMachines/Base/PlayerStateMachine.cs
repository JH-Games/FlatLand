using System;
using OknaaEXTENSIONS.CustomWrappers;
using Player.StateMachines.MoveStates;
using Player.StateMachines.WeaponStates;
using UnityEngine;

namespace Player.StateMachines.Base {
    public class PlayerStateMachine : Singleton<PlayerStateMachine> {
        public static Action<State> OnMoveStateChanged;
        public PlayerData playerData;
        
        public float Speed { get; private set; }

        public MoveState CurrentMoveState => _currentMoveState;
        private static MoveState _currentMoveState;
        private static WeaponState _currentWeaponState;

        public static void Init() {
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
            print("move: " + move);
            if (move == Vector2.zero) {
                Speed = 0;
                return;
            }
            
            if (_currentMoveState is ParkourState) {
                Speed = playerData.RunSpeed;
            } else if (_currentMoveState is SneakState) {
                Speed = playerData.SneakSpeed;
            } else if (_currentMoveState is NormalState) {
                Speed = playerData.WalkSpeed;
            }
        }
    }
}