using _Game.Scripts;
using OknaaEXTENSIONS.CustomWrappers;
using Player;
using Player.Animation;
using Player.StateMachines.Base;
using Systems.Input;
using UnityEngine;

namespace Systems {
    public class GameManager : Singleton<GameManager> {
      
        private void Awake() => Init();

        private static void Init() {
            InputSystem.Instance.Init();
            PlayerStateMachine.Instance.Init();
            State.Init(InputSystem.Instance);
            WeaponSystem.Instance.Init();
            CameraController.Instance.Init();

        }
        
        
        public override void Dispose() {
            CameraController.Instance.Dispose();
            InputSystem.Instance.Dispose();
            ThirdPersonController.Instance.Dispose();
            PlayerStateMachine.Instance.Dispose();
            
        }
    }
}