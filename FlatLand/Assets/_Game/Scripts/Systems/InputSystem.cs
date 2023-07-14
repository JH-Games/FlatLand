using Player;
using Player.Animation;
using Player.StateMachines.Base;
using Player.StateMachines.MoveStates;
using Player.StateMachines.WeaponStates;
using StarterAssetsStuff;
using UnityEngine;

namespace Systems {
    public static class InputSystem  {
        private static ThirdPersonController _playerController;
        private static StarterAssetsInputs _input;

        
        public static void Init(ThirdPersonController playerController, StarterAssetsInputs input) {
            _playerController = playerController;
            _input = input;
            
        }

        public static void CheckInput() {
            // if (_input.aim) {
            //     PlayerStateMachine.SwitchWeaponState(new AimState());
            // }
            // else if (_input.sprint) {
            //     PlayerStateMachine.SwitchMoveState(new ParkourState());
            // }
            // else if (_input.sneak) {
            //     PlayerStateMachine.SwitchMoveState(new SneakState());
            // }
            // else if (_input.idle) {
            //     PlayerStateMachine.SwitchMoveState(new IdleState());
            // }
            // else {
            //     PlayerStateMachine.SwitchMoveState(new WalkState());
            // }


            // if (_input.move == Vector2.zero) ThirdPersonController.Instance.SetTargetSpeed(0);
            
        }
    }
}