
using Player.Animation;
using Player.StateMachines.Base;
using UnityEngine;

namespace Player.StateMachines.MoveStates {
    public class IdleState : MoveState {

        public override void Enter() {

            ThirdPersonController.Instance.SetTargetSpeed(0);
            Speed = 0;
            Print("---> MoveState : IdleState ___ Enter");


        }

        

        public override void Exit() {
            Print("---> MoveState : IdleState ___ Exit");

            
        }
    }
}