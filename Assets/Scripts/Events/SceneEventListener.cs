using DevLocker.Utils;
using Events.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    /// <summary>
    /// This component adds a listener to ScriptableObject Assets of type SceneEventChannelSO
    /// to a GameObject. This allows the GameObject to react to events in this channel.
    /// </summary>
    public class SceneEventListener : MonoBehaviour
    {
        [SerializeField] private SceneEventChannelSO channel;
        public UnityEvent<SceneReference> onEventRaised;

        private void OnEnable()
        {
            if (channel != null)
            {
                channel.OnEventRaised += Respond;
            }
        }

        private void OnDisable()
        {
            if (channel != null)
            {
                channel.OnEventRaised -= Respond;
            }
        }

        private void Respond(SceneReference scene)
        {
            onEventRaised?.Invoke(scene);
        }
    }
}