using DevLocker.Utils;
using Events.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    /// <summary>
    /// This component adds a listener to ScriptableObject Assets of type IntEventChannelSO
    /// to a GameObject. This allows the GameObject to react to events in this channel.
    /// </summary>
    public class IntEventChannelListener : MonoBehaviour
    {
        [SerializeField] private IntEventChannelSO channel;
        public UnityEvent<int> onEventRaised;

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

        private void Respond(int val)
        {
            onEventRaised?.Invoke(val);
        }
    }
}