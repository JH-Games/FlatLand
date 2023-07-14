using Player.StateMachines.Base;
using Player.StateMachines.MoveStates;
using Player.StateMachines.WeaponStates;
using Systems;
using UnityEngine;
using static PlayerInputAction;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssetsStuff {
    public class StarterAssetsInputs : MonoBehaviour, IPlayerActions {
    [SerializeField] public PlayerInputAction PlayerInputAction;

    [Header("Character Input Values")]
    public Vector2 move;

    public Vector2 look;
    public bool jump;


    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;

    public bool cursorInputForLook = true;

    private bool _isInit;

    private void Awake() {
        PlayerInputAction = new PlayerInputAction();
        PlayerInputAction.Player.SetCallbacks(this);
        
        Init();
    }

    public void Init() {
        if (_isInit) return;

        var playerMap = PlayerInputAction.Player;

        // playerMap.Move.performed += OnMove;
        // playerMap.Look.performed += OnLook;
        // playerMap.Jump.performed += OnJump;
        // playerMap.Sprint.performed += OnSprint;
        // playerMap.Sprint.canceled += OnSprint;
        // playerMap.Sneak.performed += OnSneak;
        // playerMap.Sneak.canceled += OnSneak;
        // playerMap.Aim.performed += OnAim;
        // playerMap.Aim.canceled += OnAim;

        _isInit = true;
    }

    private void OnApplicationFocus(bool hasFocus) {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState) {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void OnMove(InputAction.CallbackContext context) {
        move = context.ReadValue<Vector2>();

        if (!GameStateSystem.IsPlaying) return;

        if (PlayerStateMachine.Instance.CurrentMoveState is IdleState)
            PlayerStateMachine.SwitchMoveState(new WalkState());
        else if (move == Vector2.zero)
            PlayerStateMachine.SwitchMoveState(new IdleState());
    }

    public void OnLook(InputAction.CallbackContext context) {
        if (cursorInputForLook) {
            look = context.ReadValue<Vector2>();
        }
    }

    public void OnJump(InputAction.CallbackContext context) {
        jump = context.ReadValueAsButton();
    }

    public void OnSprint(InputAction.CallbackContext context) {
        if (!GameStateSystem.IsPlaying) return;

        switch (context) {
            case { phase: InputActionPhase.Started }:
                PlayerStateMachine.SwitchMoveState(new ParkourState());
                break;
            case { phase: InputActionPhase.Canceled }:
                PlayerStateMachine.SwitchMoveState(new WalkState());
                break;
        }
    }

    public void OnSneak(InputAction.CallbackContext context) {
        if (!GameStateSystem.IsPlaying) return;

        switch (context) {
            case { phase: InputActionPhase.Started }:
                PlayerStateMachine.SwitchMoveState(new SneakState());
                break;
            case { phase: InputActionPhase.Canceled }:
                PlayerStateMachine.SwitchMoveState(new WalkState());
                break;
        }
    }

    public void OnAim(InputAction.CallbackContext context) {
        if (!GameStateSystem.IsPlaying) return;

        switch (context) {
            case { phase: InputActionPhase.Started }:
                PlayerStateMachine.SwitchWeaponState(new AimState());
                break;
            case { phase: InputActionPhase.Canceled }:
                PlayerStateMachine.SwitchWeaponState(new UnsheathedState());
                break;
        }
    }

    public void OnUnsheath(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    private void OnDestroy() {
        Dispose();
    }

    public void Dispose() {
        if (!_isInit) return;
        // PlayerInputAction.Player.Move.performed -= OnMove;
        // PlayerInputAction.Player.Look.performed -= OnLook;
        // PlayerInputAction.Player.Jump.performed -= OnJump;
        // PlayerInputAction.Player.Sprint.performed -= OnSprint;
        // PlayerInputAction.Player.Sprint.canceled -= OnSprint;
        // PlayerInputAction.Player.Sneak.performed -= OnSneak;
        // PlayerInputAction.Player.Sneak.canceled -= OnSneak;
        // PlayerInputAction.Player.Aim.performed -= OnAim;
        // PlayerInputAction.Player.Aim.canceled -= OnAim;

        _isInit = false;
    }
    }
}