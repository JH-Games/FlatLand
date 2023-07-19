using Cinemachine;
using OknaaEXTENSIONS.CustomWrappers;
using Player.StateMachines.Base;
using Player.StateMachines.MoveStates;
using Player.StateMachines.WeaponStates;
using Systems.Input;
using UnityEngine;

namespace Systems {
    public class CameraController : Singleton<CameraController> {
        [SerializeField] private CinemachineVirtualCamera normalCamera;
        [SerializeField] private CinemachineVirtualCamera sprintCamera;
        [SerializeField] private CinemachineVirtualCamera sneakCamera;
        [SerializeField] private CinemachineVirtualCamera aimCamera;

        [Space(10)]

        public bool LockCameraPosition = false;
        public float TopClamp = 70.0f;
        public float BottomClamp = -30.0f;

        private GameObject CameraTarget;
        private const float _threshold = 0.01f;
        private static float _targetYaw;
        private float _targetPitch;
        private bool _isInitialized;

        public void Init() {
            if (_isInitialized) return;
            CameraTarget = PlayerStateMachine.Instance.CameraTarget;
            PlayerStateMachine.OnMoveStateChanged += HandleCameraModeChanged;
            _isInitialized = true;
        }

        private void HandleCameraModeChanged(State currentState) {
            normalCamera.Priority = currentState is NormalState ? 1 : 0;
            sprintCamera.Priority = currentState is ParkourState ? 1 : 0;
            sneakCamera.Priority = currentState is SneakState ? 1 : 0;
            aimCamera.Priority = currentState is AimState ? 1 : 0;
        }

        private void LateUpdate() {
            // make the player rotate with the _input.look when aiming
            if (InputSystem.Instance.look.sqrMagnitude >= _threshold && !LockCameraPosition) {
                _targetYaw += InputSystem.Instance.look.x;
                _targetPitch += InputSystem.Instance.look.y;
            }

            _targetYaw = ClampAngle(_targetYaw, float.MinValue, float.MaxValue);
            _targetPitch = ClampAngle(_targetPitch, BottomClamp, TopClamp);

            CameraTarget.transform.rotation = Quaternion.Euler(_targetPitch, _targetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax) {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        public override void Dispose() {
            if (!_isInitialized) return;
            PlayerStateMachine.OnMoveStateChanged -= HandleCameraModeChanged;
            _isInitialized = false;
        }
    }
}