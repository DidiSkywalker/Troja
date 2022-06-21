using UnityEngine;
using Enums;
using UnityEngine.Events;

namespace Events.Channels
{
    /// <summary>
    /// This class implements a channel for events with a parameter of type String
    /// </summary>

    [CreateAssetMenu(menuName = "Events/Movement Event Channel")]
    public class MovementEventChannelSO : ScriptableObject
    {
        public UnityAction<AnimatedMovements> OnEventRaised;

        /// <summary>
        /// Raise an event in this channel.
        /// Any Listeners listening to this channel will be notified.
        /// </summary>
        public void RaiseMovementEvent(AnimatedMovements movement)
        {
            OnEventRaised?.Invoke(movement);
        }
    }
}