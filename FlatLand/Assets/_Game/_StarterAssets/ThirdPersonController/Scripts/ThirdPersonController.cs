using OknaaEXTENSIONS.CustomWrappers;
using Player.Animation;
using Player.StateMachines.Base;
using Player.StateMachines.MoveStates;
using Player.StateMachines.WeaponStates;
using Systems;
using UnityEngine;
using Systems.Input;
using InputSystem = Systems.Input.InputSystem;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace Player {
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : Singleton<ThirdPersonController> {
      
    
        
     


      
     

       

      




    
    }
}