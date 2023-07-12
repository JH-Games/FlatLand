using _Game.Scripts;
using Cinemachine;
using OknaaEXTENSIONS.CustomWrappers;
using UnityEngine;

public class CameraController : Singleton<CameraController> {
    [SerializeField] private CinemachineVirtualCamera normalCamera;
    [SerializeField] private CinemachineVirtualCamera sprintCamera;
    [SerializeField] private CinemachineVirtualCamera sneakCamera;

    private void Start() => PlayerStates.OnPlayerStateChanged += HandleCameraModeChanged;

    private void HandleCameraModeChanged(PlayerState playerState) {
        normalCamera.Priority = playerState is PlayerState.Idle or PlayerState.Walk ? 1 : 0;
        sprintCamera.Priority = playerState is PlayerState.Sprint ? 1 : 0;
        sneakCamera.Priority = playerState is PlayerState.Sneak ? 1 : 0;
    }

    protected override void Dispose() => PlayerStates.OnPlayerStateChanged -= HandleCameraModeChanged;
}