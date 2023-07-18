
using _Game.Scripts;
using Player.Animation;
using UnityEngine;

namespace Player.StateMachines.WeaponStates {
    public class SheathedState : WeaponState {

        public override void Enter() {
            Print("SheathedState Enter");
            PlayerAnimation.SetUnsheathed(false);
            WeaponSystem.Instance.UnEquip();
        }

        public override void Tick(Transform transform, float deltaTime) {
        }

        public override void Exit() {
            
        }
    }
}