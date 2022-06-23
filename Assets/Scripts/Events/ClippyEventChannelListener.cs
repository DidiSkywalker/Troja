using Events.Channels;
using ClippyAction;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    /// <summary>
    /// This component adds a listener to ScriptableObject Assets of type ClippyEventChannelSO
    /// to a GameObject. This allows the GameObject to react to events in this channel.
    /// </summary>
    public class ClippyEventChannelListener : MonoBehaviour
    {
        [SerializeField] private StringEventChannelSO channel;
        public UnityEvent<string> onEventRaised;

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

        private void Respond(string message)
        {
            onEventRaised?.Invoke(message);
        }
    }
}