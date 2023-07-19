using OknaaEXTENSIONS.CustomWrappers;
using UnityEngine;

namespace Systems {
    public class SoundSystem : Singleton<SoundSystem> {
        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        public void PlayFootStep(AnimationEvent animationEvent, Vector3 transformPoint) {
            if (!(animationEvent.animatorClipInfo.weight > 0.5f)) return;
            if (FootstepAudioClips.Length <= 0) return;

            var index = Random.Range(0, FootstepAudioClips.Length);
            AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transformPoint, FootstepAudioVolume);
        }


        public void PlayLand(AnimationEvent animationEvent, Vector3 transformPoint) {
            if (!(animationEvent.animatorClipInfo.weight > 0.5f)) return;

            AudioSource.PlayClipAtPoint(LandingAudioClip, transformPoint, FootstepAudioVolume);
        }
    }
}