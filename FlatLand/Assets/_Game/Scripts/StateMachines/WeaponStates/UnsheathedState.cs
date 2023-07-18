
using _Game.Scripts;
using Player.Animation;
using UnityEngine;

namespace Player.StateMachines.WeaponStates {
    public class UnsheathedState : WeaponState {

        public override void Enter() {
            Print("UnsheathedState Enter");
            PlayerAnimation.SetUnsheathed(false);

            // WeaponSystem.Instance.Equip();
        }

        public override void Tick(Transform transform, float deltaTime) {
        }

        public override void Exit() {
            
        }
    }
}