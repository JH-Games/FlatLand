using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using OknaaEXTENSIONS.CustomWrappers;
using UnityEngine;

public enum CameraMode {
    Normal,
    Sprint,
    Sneak
}
public class CameraController : Singleton<CameraController> {
    public static Action<CameraMode> OnCameraModeChanged;
    [SerializeField] private CinemachineVirtualCamera normalCamera;
    [SerializeField] private CinemachineVirtualCamera sprintCamera;
    [SerializeField] private CinemachineVirtualCamera sneakCamera;

    private void Start() {
        OnCameraModeChanged += HandleCameraModeChanged;
    }

    private void HandleCameraModeChanged(CameraMode cameraMode) {
        normalCamera.Priority = cameraMode == CameraMode.Normal ? 1 : 0;
        sprintCamera.Priority = cameraMode == CameraMode.Sprint ? 1 : 0;
        sneakCamera.Priority = cameraMode == CameraMode.Sneak ? 1 : 0;
    }

    protected override void Dispose() {
        OnCameraModeChanged -= HandleCameraModeChanged;
    }
}