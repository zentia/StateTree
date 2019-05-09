#if UNITY_4_6 || UNITY_4_7
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
    [TaskCategory("Basic/AudioSource")]
    [TaskDescription("Stores the pan value of the AudioSource. Returns Success.")]
    public class GetPan : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The pan value of the AudioSource")]
        [RequiredField]
        public SharedFloat storeValue;

        private AudioSource audioSource;
        private GameObject prevGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                audioSource = currentGameObject.GetComponent<AudioSource>();
                prevGameObject = currentGameObject;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (audioSource == null) {
                UnityEngine.Debug.LogWarning("AudioSource is null");
                return TaskStatus.Failure;
            }

            storeValue.Value = audioSource.pan;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            storeValue = 1;
        }
    }
}
#endif