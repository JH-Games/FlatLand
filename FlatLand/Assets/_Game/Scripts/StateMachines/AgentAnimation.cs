using System.Collections;
using UnityEngine;

namespace StateMachines
{
    public class AgentAnimation : MonoBehaviour
    {
        public Animator animator;

        public float transitionSpeed = 5f;
        private float currentSpeed = 0f;

        bool isAttacked = false;

        public void SetIdle()
        {
            TransitionToAnimation(0f);
        }

        public void SetWalk()
        {
            TransitionToAnimation(0.5f);
        }

        public void SetRun()
        {
            TransitionToAnimation(1f);
        }
        public void Attack()
        {
            isAttacked = true;
            animator.SetBool("attack", true);
        }

        private void TransitionToAnimation(float targetSpeed)
        {
            StopAllCoroutines();
            StartCoroutine(AnimateTransition(targetSpeed));
        }

        private IEnumerator AnimateTransition(float targetSpeed)
        {
            float elapsedTime = 0f;
            float startSpeed = currentSpeed;

            while (elapsedTime < transitionSpeed)
            {
                elapsedTime += Time.deltaTime;
                currentSpeed = Mathf.Lerp(startSpeed, targetSpeed, elapsedTime / transitionSpeed);
                animator.SetFloat("speed", currentSpeed);
                yield return null;
            }

            currentSpeed = targetSpeed;
            animator.SetFloat("speed", currentSpeed);
        }
    }
}
