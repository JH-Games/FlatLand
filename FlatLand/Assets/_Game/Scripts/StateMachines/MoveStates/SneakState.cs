using Player.Animation;
using Player.StateMachines.Base;
using UnityEngine;

namespace Player.StateMachines.MoveStates {
    public class SneakState : MoveState {

        public override void Enter() {
            PlayerAnimation.SetSneak(true);
            // ThirdPersonController.Instance.SetTargetSpeed(PlayerStateMachine.Instance.playerData.SneakSpeed);
            // Speed = PlayerStateMachine.Instance.playerData.SneakSpeed;

        }


        public override void Exit() {
            PlayerAnimation.SetSneak(false);
        }
    }
}