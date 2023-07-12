using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets {
    public class StarterAssetsInputs : MonoBehaviour {
        [Header("Character Input Values")]
        public Vector2 move;

        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool sneak;
        public bool aim;
        public bool unsheathed;


        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;

        public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value) {
            move = value.Get<Vector2>();
        }

        public void OnLook(InputValue value) {
            if (cursorInputForLook) {
                look = value.Get<Vector2>();
            }
        }

        public void OnJump(InputValue value) {
            jump = value.isPressed;
        }

        public void OnSprint(InputValue value) {
            sprint = value.isPressed;
            sneak = false;
        }

        public void OnSneak(InputValue value) {
            sneak = value.isPressed;
        }

        public void OnAim(InputValue value) {
            aim = value.isPressed;
        }

        public void OnUnsheath(InputValue value) {
            unsheathed = value.isPressed;
        }

#endif

        private void OnApplicationFocus(bool hasFocus) {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState) {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}