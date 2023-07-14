using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player.StateMachines.Base {
    
    public abstract class State {
        public abstract void Enter();
        public abstract void Tick(Transform transform_, float deltaTime);
        public abstract void Exit();
        
        protected void Print(string message) {
            Debug.Log(message);
        }
    }
}