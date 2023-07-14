using Cinemachine;
using OknaaEXTENSIONS.CustomWrappers;
using Player.StateMachines.Base;
using Player.StateMachines.MoveStates;
using Player.StateMachines.WeaponStates;
using UnityEngine;

namespace Systems {
    public class CameraController : Singleton<CameraController> {
        [SerializeField] private CinemachineVirtualCamera normalCamera;
        [SerializeField] private CinemachineVirtualCamera sprintCamera;
        [SerializeField] private CinemachineVirtualCamera sneakCamera;
        [SerializeField] private CinemachineVirtualCamera aimCamera;


        private void Start() => PlayerStateMachine.OnMoveStateChanged += HandleCameraModeChanged;

        private void HandleCameraModeChanged(State currentState) {
            normalCamera.Priority = currentState is IdleState or WalkState ? 1 : 0;
            sprintCamera.Priority = currentState is ParkourState ? 1 : 0;
            sneakCamera.Priority = currentState is SneakState ? 1 : 0;
            aimCamera.Priority = currentState is AimState ? 1 : 0;
        }

        protected override void Dispose() => PlayerStateMachine.OnMoveStateChanged -= HandleCameraModeChanged;
    }
}