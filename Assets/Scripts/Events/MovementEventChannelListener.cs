using Events.Channels;
using Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    /// <summary>
    /// This component adds a listener to ScriptableObject Assets of type MovementEventChannelSO
    /// to a GameObject. This allows the GameObject to react to events in this channel.
    /// </summary>
    public class MovementEventChannelListener : MonoBehaviour
    {
        [SerializeField] private MovementEventChannelSO channel;
        public UnityEvent<AnimatedMovements> onEventRaised;

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

        private void Respond(AnimatedMovements movement)
        {
            onEventRaised?.Invoke(movement);
        }
    }
}