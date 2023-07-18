using System;
using OknaaEXTENSIONS.CustomWrappers;
using Player.StateMachines.Base;
using Player.StateMachines.MoveStates;
using Player.StateMachines.WeaponStates;
using Systems.Input;
using UnityEngine;
using static Systems.Input.PlayerInputAction;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Systems.Input {
    public class InputSystem : Singleton<InputSystem>, IPlayerActions {
        private static PlayerInputAction PlayerInputAction;


        [Header("Character Input Values")]
        public Vector2 move;

        public Vector2 look;
        public bool jump;
        private bool _IsMoving => move != Vector2.zero;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;

        public bool cursorInputForLook = true;

        private static bool _isInit;

        public void Init() {
            if (_isInit) return;

            PlayerInputAction = new PlayerInputAction();
            var playerMap = PlayerInputAction.Player;

            playerMap.SetCallbacks(this);

            // playerMap.Move.performed += OnMove;
            // playerMap.Look.performed += OnLook;
            // playerMap.Jump.performed += OnJump;
            // playerMap.Sprint.performed += OnSprint;
            // playerMap.Sprint.canceled += OnSprint;
            // playerMap.Sneak.performed += OnSneak;
            // playerMap.Sneak.canceled += OnSneak;
            // playerMap.Aim.performed += OnAim;
            // playerMap.Aim.canceled += OnAim;

            playerMap.Move.Enable();
            playerMap.Look.Enable();
            playerMap.Jump.Enable();
            playerMap.Sprint.Enable();
            playerMap.Sneak.Enable();
            playerMap.Aim.Enable();

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

            // if (move != Vector2.zero) {
            //     PlayerStateMachine.SwitchMoveState(new NormalState());
            // }
            // else {
            // PlayerStateMachine.SwitchMoveState(new IdleState());
            // }

            PlayerStateMachine.Instance.UpdateSpeed(move);


            //
            // if (PlayerStateMachine.Instance.CurrentMoveState is IdleState) 
            //     PlayerStateMachine.SwitchMoveState(new WalkState());
            // else if (move == Vector2.zero) 
            //     PlayerStateMachine.SwitchMoveState(new IdleState());
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
                    PlayerStateMachine.SwitchMoveState(new NormalState());
                    break;
            }

            PlayerStateMachine.Instance.UpdateSpeed(move);
        }

        public void OnSneak(InputAction.CallbackContext context) {
            if (!GameStateSystem.IsPlaying) return;

            switch (context) {
                case { phase: InputActionPhase.Started }:
                    PlayerStateMachine.SwitchMoveState(new SneakState());
                    break;
                case { phase: InputActionPhase.Canceled }:
                    PlayerStateMachine.SwitchMoveState(new NormalState());
                    break;
            }

            PlayerStateMachine.Instance.UpdateSpeed(move);
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
            if (!GameStateSystem.IsPlaying) return;
            
            PlayerStateMachine.SwitchWeaponState(
                PlayerStateMachine.CurrentWeaponState is SheathedState
                    ? new UnsheathedState()
                    : new SheathedState()
                );
        }

        public override void Dispose() {
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

            PlayerInputAction.Player.Move.Disable();
            PlayerInputAction.Player.Look.Disable();
            PlayerInputAction.Player.Jump.Disable();
            PlayerInputAction.Player.Sprint.Disable();
            PlayerInputAction.Player.Sneak.Disable();
            PlayerInputAction.Player.Aim.Disable();

            _isInit = false;
        }
    }
}