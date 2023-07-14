using Player.StateMachines.Base;
using StarterAssetsStuff;
using UnityEngine;

namespace Player.StateMachines.WeaponStates {
    
    public abstract class WeaponState : State {
        protected StarterAssetsInputs _input;
        protected CharacterController CharacterController => _characterController ??= ThirdPersonController.Instance.CharacterController;
        private CharacterController _characterController;
        
        protected Transform Transform;
        protected Camera Camera => _camera ??= Camera.main;
        private Camera _camera;
        protected static float RotationSmoothTime => ThirdPersonController.Instance.RotationSmoothTime;
        protected static float SpeedChangeRate => ThirdPersonController.Instance.SpeedChangeRate;
        protected static float VerticalVelocity => ThirdPersonController.Instance.VerticalVelocity;
        
        protected static float TargetRotation {
            get => ThirdPersonController.Instance.TargetRotation;
            set => ThirdPersonController.Instance.TargetRotation = value;
        }
        protected static float TargetSpeed {
            get => ThirdPersonController.Instance.TargetSpeed;
            set => ThirdPersonController.Instance.TargetSpeed = value;
        }
        
        protected static float RotationVelocity {
            get => ThirdPersonController.Instance.RotationVelocity;
            set => ThirdPersonController.Instance.RotationVelocity = value;
        }
        protected static float Speed {
            get => ThirdPersonController.Instance.Speed;
            set => ThirdPersonController.Instance.Speed = value;
        } 
        protected static float AnimationBlend {
            get => ThirdPersonController.Instance.AnimationBlend;
            set => ThirdPersonController.Instance.AnimationBlend = value;
        }
        
        protected  WeaponState() {
            _input = ThirdPersonController.Instance.Input;
        }

        public override void Tick(Transform transform_, float deltaTime) {
            Transform ??= transform_;
        }
    }
}