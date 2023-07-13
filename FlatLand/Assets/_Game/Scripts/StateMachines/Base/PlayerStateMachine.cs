using System;
using OknaaEXTENSIONS.CustomWrappers;
using UnityEngine;

namespace StateMachines.Base {
    public class PlayerStateMachine : Singleton<PlayerStateMachine> {
        public static Action<State> OnStateChanged;

        private static State currentState;

        private void Update() {
            currentState?.Tick(Time.deltaTime);
        }

        public static void SwitchState(State state) {
            currentState?.Exit();
            currentState = state;
            currentState?.Enter();
            OnStateChanged?.Invoke(state);
        }
    }
}