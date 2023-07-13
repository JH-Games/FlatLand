using Player.Animation;
using StateMachines.Base;

namespace Player.StateMachines.MoveStates {
    public class SneakState : MoveState {

        public override void Enter() {
            PlayerAnimation.SetSneak(true);
        }

        public override void Tick(float deltaTime) {
        }

        public override void Exit() {
            PlayerAnimation.SetSneak(false);
        }
    }
}