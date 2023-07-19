using Player.Animation;
using Player.StateMachines.Base;
using Systems;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Systems.Input.InputSystem;

namespace Player.StateMachines.MoveStates {
    public abstract class MoveState : State {
        private static float StateSpeed => PlayerStateMachine.Instance.Speed;

        private static CharacterController _characterController;
        private static Transform Transform;
        private static Camera _camera;
        
     
        #region PlayerData Parameters

        private static float Speed;
        private static float SpeedChangeRate;
        private static float RotationSmoothTime;
        private static float JumpHeight;
        private static float TimeUntilJumpAgain;
        private static float TimeUntilFallAgain;
        private static float Gravity;
        private static float TerminalVelocity;
        private static bool Grounded = true;
        private static float GroundedYOffset;
        private static float GroundedCheckRadius;
        private static LayerMask GroundLayers;

        #endregion


        private const float _speedOffset = 0.1f;
        private static float TargetRotation;
        private static float AnimationBlend;
        private static float _rotationVelocity;
        private static float _verticalVelocity;



        private static float _jumpTimeoutDelta;
        private static float _fallTimeoutDelta;

        private static PlayerData _playerData;

        public static void Init() {
            _characterController = PlayerStateMachine.CharacterController;
            _playerData = PlayerStateMachine.Instance.playerData;
            
            RotationSmoothTime = _playerData.RotationSmoothTime;
            SpeedChangeRate = _playerData.SpeedChangeRate;
            TimeUntilJumpAgain = _playerData.TimeUntilJumpAgain;
            TimeUntilFallAgain = _playerData.TimeUntilFallAgain;
            JumpHeight = _playerData.JumpHeight;
            Gravity = _playerData.Gravity;
            TerminalVelocity = _playerData._terminalVelocity;
            Grounded = _playerData.Grounded;
            GroundedYOffset = _playerData.GroundedYOffset;
            GroundedCheckRadius = _playerData.GroundedCheckRadius;
            GroundLayers = _playerData.GroundLayers;
            
            _camera = Camera.main;
            Transform = PlayerStateMachine.Instance.transform;
        }
        
        public override void Tick(Transform transform_, float deltaTime) {

            JumpAndGravity();
            BlendSpeedOverTime();
            HandleMovement();
        }


        private void JumpAndGravity() {
            if (Grounded) {
                _fallTimeoutDelta = TimeUntilFallAgain;

                PlayerAnimation.SetJump((false));
                PlayerAnimation.SetFreeFall((false));

                if (_verticalVelocity < 0.0f) _verticalVelocity = -2f;

                if (InputSystem.Instance.jump && _jumpTimeoutDelta <= 0.0f) {
                    var velocityNeededToReachHeight = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                    _verticalVelocity = velocityNeededToReachHeight;

                    PlayerAnimation.SetJump((true));
                }

                if (_jumpTimeoutDelta >= 0.0f) _jumpTimeoutDelta -= Time.deltaTime;
            }
            else {
                _jumpTimeoutDelta = TimeUntilJumpAgain;

                if (_fallTimeoutDelta >= 0.0f) _fallTimeoutDelta -= Time.deltaTime;
                else PlayerAnimation.SetFreeFall((true));

                InputSystem.Instance.jump = false;
            }

            if (_verticalVelocity < TerminalVelocity) _verticalVelocity += Gravity * Time.deltaTime;
            
            GroundedCheck();
        }

        private void GroundedCheck() {
            Vector3 spherePosition = new Vector3(Transform.position.x, Transform.position.y - GroundedYOffset, Transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedCheckRadius, GroundLayers, QueryTriggerInteraction.Ignore);
            PlayerAnimation.SetGrounded(Grounded);
        }

        
        private void BlendSpeedOverTime() {
            var velocity = _characterController.velocity;
            var currentSpeed = new Vector3(velocity.x, 0.0f, velocity.z).magnitude;
            var inputMagnitude = Input.analogMovement ? Input.move.magnitude : 1f;
            var targetSpeed = StateSpeed * inputMagnitude;
            PlayerAnimation.SetMotionSpeed(inputMagnitude);

            var targetSpeedReached = currentSpeed < StateSpeed - _speedOffset || currentSpeed > StateSpeed + _speedOffset;
            if (targetSpeedReached) Speed = StateSpeed;
            else {
                Speed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * SpeedChangeRate);
                Speed = Mathf.Round(Speed * 1000f) / 1000f;
            }

            AnimationBlend = Mathf.Lerp(AnimationBlend, StateSpeed, Time.deltaTime * SpeedChangeRate);
            if (AnimationBlend < 0.01f) AnimationBlend = 0f;
            PlayerAnimation.SetSpeed(AnimationBlend);
        }
        private void HandleMovement() {
            if (Input.IsMoving) {
                var inputDirection = new Vector3(Input.move.x, 0.0f, Input.move.y).normalized;

                TargetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
                Transform.rotation = Quaternion.Euler(
                    0.0f,
                    Mathf.SmoothDampAngle(Transform.eulerAngles.y, TargetRotation, ref _rotationVelocity, RotationSmoothTime),
                    0.0f);
            }

            var horizontalMovement = (Quaternion.Euler(0.0f, TargetRotation, 0.0f) * Vector3.forward).normalized * Speed;
            var verticalMovement = new Vector3(0.0f, _verticalVelocity, 0.0f);
            _characterController.Move(Time.deltaTime * (horizontalMovement + verticalMovement));
        }
    }
}