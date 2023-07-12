using System;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    public Animator animator;
    
    public static readonly int MovementBlend = Animator.StringToHash("MovementBlend");
    
    

    public void SetBlendValue(float value) {
        animator.SetFloat(MovementBlend, value);
    }
}   