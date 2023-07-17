using Player.Animation;
using Player.StateMachines.Base;
using UnityEngine;

namespace Player.StateMachines.MoveStates {
    public class NormalState : MoveState {
        
        public NormalState() : base() {
        }

        public override void Enter() {
            // ThirdPersonController.Instance.SetTargetSpeed(PlayerStateMachine.Instance.playerData.WalkSpeed);
            // Speed = PlayerStateMachine.Instance.playerData.WalkSpeed;
            Print("---> MoveState : WalkState ___ Enter");
        }

        

        public override void Exit() {
            Print("---> MoveState : WalkState ___ Exit");
        }
    }
}