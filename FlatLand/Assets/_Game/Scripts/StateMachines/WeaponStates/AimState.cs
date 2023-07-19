using Player.Animation;
using Player.StateMachines.Base;
using UnityEngine;

namespace Player.StateMachines.WeaponStates {
    public class AimState : WeaponState {
        private float _rotationVelocity;

        public override void Enter() {
            Print("AimState Enter");
            PlayerAnimation.SetAim(true);
        }

        public override void Tick(Transform transform, float deltaTime) {
        }

        public override void Exit() {
            PlayerAnimation.SetAim(false);
        }
    }
}