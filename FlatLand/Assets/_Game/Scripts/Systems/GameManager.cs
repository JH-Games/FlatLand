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
            CameraController.Instance.Init();
            InputSystem.Instance.Init();
            State.Init(InputSystem.Instance);
            ThirdPersonController.Instance.Init();
            PlayerAnimation.Init(ThirdPersonController.Instance.Animator);
            PlayerStateMachine.Init();
            WeaponSystem.Instance.Init();
        }
        
        
        public override void Dispose() {
            CameraController.Instance.Dispose();
            InputSystem.Instance.Dispose();
            ThirdPersonController.Instance.Dispose();
            PlayerStateMachine.Instance.Dispose();
            
        }
    }
}