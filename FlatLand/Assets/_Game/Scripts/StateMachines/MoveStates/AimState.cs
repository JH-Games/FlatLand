using Player.Animation;

namespace Player.StateMachines.WeaponStates {
    public class AimState : WeaponState {

        public override void Enter() {
            PlayerAnimation.SetAim(true);
        }

        public override void Tick(float deltaTime) {
        }

        public override void Exit() {
            PlayerAnimation.SetAim(false);
        }
    }
}