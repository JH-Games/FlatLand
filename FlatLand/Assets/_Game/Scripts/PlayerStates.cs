using System;
using OknaaEXTENSIONS.CustomWrappers;
using UnityEngine;

namespace _Game.Scripts {
    public enum PlayerState {
        Idle,
        Walk,
        Sprint,
        Sneak,
        Aim,
    }

    public static class PlayerStates {
        public static Action<PlayerState> OnPlayerStateChanged;
        
        private static PlayerState playerState = PlayerState.Idle;


        public static void SetState(PlayerState newState) {
            if (playerState == newState) return;

            playerState = newState;
            OnPlayerStateChanged?.Invoke(playerState);
        }

    }
}