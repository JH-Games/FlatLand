

using Player.StateMachines.Base;
using UnityEngine;

namespace Player.StateMachines.MoveStates {
    public class ParkourState : MoveState {

        public override void Enter() {

            ThirdPersonController.Instance.SetTargetSpeed(PlayerStateMachine.Instance.playerData.RunSpeed);
            Speed = PlayerStateMachine.Instance.playerData.RunSpeed;

        }

       

        public override void Exit() {
            
        }
    }
}