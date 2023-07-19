using System;
using UnityEngine;

namespace Player {
    
    [Serializable]
    public class PlayerData {
        [Header("States Movement Speed")]
        public float WalkSpeed;
        public float RunSpeed;
        public float SneakSpeed;
        public float AimSpeed;
        [Space(10)]
        public float SpeedChangeRate = 10.0f;
        [Range(0.0f, 0.3f)] public float RotationSmoothTime = 0.12f;

        [Header("Jump")]
        public float JumpHeight = 1.2f;
        [HideInInspector] public float TimeUntilJumpAgain = 0.50f;
        [HideInInspector] public float TimeUntilFallAgain = 0.15f;
        public float Gravity = -15.0f;
        public float _terminalVelocity = 53.0f;


        [Header("Grounded")]
        public bool Grounded = true;
        public float GroundedYOffset = -0.14f;
        public float GroundedCheckRadius = 0.28f;
        public LayerMask GroundLayers;
        
    }
    
}