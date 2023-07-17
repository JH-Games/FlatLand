using Systems.Input;
using UnityEngine;

namespace Player.StateMachines.Base {
    public abstract class State {
        protected static InputSystem Input;
        public static void Init(InputSystem input) => Input = input;


        public abstract void Enter();
        public abstract void Tick(Transform transform_, float deltaTime);
        public abstract void Exit();

        protected void Print(string message) {
            Debug.Log(message);
        }
    }
}