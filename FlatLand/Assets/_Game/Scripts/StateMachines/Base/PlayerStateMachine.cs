using System;
using OknaaEXTENSIONS.CustomWrappers;
using Player.StateMachines.MoveStates;
using Player.StateMachines.WeaponStates;
using UnityEngine;

namespace Player.StateMachines.Base {
    public class PlayerStateMachine : Singleton<PlayerStateMachine> {
        public static Action<State> OnMoveStateChanged;
        
        public PlayerData playerData;
        public MoveState CurrentMoveState => _currentMoveState;

        private static MoveState _currentMoveState;
        private static WeaponState _currentWeaponState;
        
        
        

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
    }
}