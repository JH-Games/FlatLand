using Player.Animation;
using Player.StateMachines.Base;
using Systems;
using UnityEngine;

namespace Player.StateMachines.MoveStates {
    public abstract class MoveState : State {
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

        private static float TargetSpeed => PlayerStateMachine.Instance.Speed;

        protected static float RotationVelocity {
            get => ThirdPersonController.RotationVelocity;
            set => ThirdPersonController.RotationVelocity = value;
        }
        protected static float Speed { get; set; }

        protected static float AnimationBlend {
            get => ThirdPersonController.AnimationBlend;
            set => ThirdPersonController.AnimationBlend = value;
        }
        
        private float _rotationVelocity;

        public override void Tick(Transform transform_, float deltaTime) {
            Transform ??= transform_;
            
            ThirdPersonController.Instance.JumpAndGravity();
            
            BlendSpeedOverTime();

            Vector3 inputDirection = new Vector3(Input.move.x, 0.0f, Input.move.y).normalized;

            TargetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + Camera.transform.eulerAngles.y;

            float rotation = Mathf.SmoothDampAngle(Transform.eulerAngles.y, TargetRotation, ref _rotationVelocity, RotationSmoothTime);
            Transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

            Vector3 targetDirection = Quaternion.Euler(0.0f, TargetRotation, 0.0f) * Vector3.forward;

            CharacterController.Move(targetDirection.normalized * (Speed * Time.deltaTime) + new Vector3(0.0f, VerticalVelocity, 0.0f) * Time.deltaTime);


            void BlendSpeedOverTime() {
                float currentHorizontalSpeed = new Vector3(CharacterController.velocity.x, 0.0f, CharacterController.velocity.z).magnitude;
                float speedOffset = 0.1f;
                float inputMagnitude = Input.analogMovement ? Input.move.magnitude : 1f;
                PlayerAnimation.SetMotionSpeed(inputMagnitude);

                var targetSpeedReached = currentHorizontalSpeed < TargetSpeed - speedOffset || currentHorizontalSpeed > TargetSpeed + speedOffset;
                if (targetSpeedReached) Speed = TargetSpeed;
                else {
                    Speed = Mathf.Lerp(currentHorizontalSpeed, TargetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
                    Speed = Mathf.Round(Speed * 1000f) / 1000f;
                }

                AnimationBlend = Mathf.Lerp(AnimationBlend, TargetSpeed, Time.deltaTime * SpeedChangeRate);
                if (AnimationBlend < 0.01f) AnimationBlend = 0f;
                PlayerAnimation.SetSpeed(AnimationBlend);
            }
        }
    }
}