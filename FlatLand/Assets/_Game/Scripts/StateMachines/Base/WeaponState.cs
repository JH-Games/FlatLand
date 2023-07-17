using Player.StateMachines.Base;
using Systems;
using UnityEngine;

namespace Player.StateMachines.WeaponStates {
    
    public abstract class WeaponState : State {
        protected CharacterController CharacterController => _characterController ??= ThirdPersonController.CharacterController;
        private CharacterController _characterController;
        
        protected Transform Transform;
        protected Camera Camera => _camera ??= Camera.main;
        private Camera _camera;
        protected static float RotationSmoothTime => ThirdPersonController.Instance.RotationSmoothTime;
        protected static float SpeedChangeRate => ThirdPersonController.Instance.SpeedChangeRate;
        protected static float VerticalVelocity => ThirdPersonController.VerticalVelocity;
        
        protected static float TargetRotation {
            get => ThirdPersonController.TargetRotation;
            set => ThirdPersonController.TargetRotation = value;
        }
        protected static float TargetSpeed {
            get => ThirdPersonController.TargetSpeed;
            set => ThirdPersonController.TargetSpeed = value;
        }
        
        protected static float RotationVelocity {
            get => ThirdPersonController.RotationVelocity;
            set => ThirdPersonController.RotationVelocity = value;
        }
        protected static float Speed {
            get => ThirdPersonController.Speed;
            set => ThirdPersonController.Speed = value;
        } 
        protected static float AnimationBlend {
            get => ThirdPersonController.AnimationBlend;
            set => ThirdPersonController.AnimationBlend = value;
        }
        
        

        public override void Tick(Transform transform_, float deltaTime) {
            Transform ??= transform_;
        }
    }
}