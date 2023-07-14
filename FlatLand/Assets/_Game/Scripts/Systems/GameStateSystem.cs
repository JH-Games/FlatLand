using System;

namespace Systems {
    
    public enum GameState {
        Playing,
        Paused,
        GameOver,
        GameWon
    }
    public static class GameStateSystem {
        public static Action<GameState> OnGameStateChanges;
        
        
        private static GameState _current = GameState.Playing;
        public static GameState Current {
            get => _current;
            set => SetGameState(value);
        }

        private static void SetGameState(GameState state) {
            if (_current == state) return;
            
            _current = state;
            OnGameStateChanges?.Invoke(state);
        }
        
        public static bool IsPlaying => _current == GameState.Playing;
    }
}