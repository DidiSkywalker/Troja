using UnityEngine;
using UnityEngine.Events;

namespace Events.Channels
{
    /// <summary>
    /// This class implements a channel for events with a parameter of type String
    /// </summary>

    [CreateAssetMenu(menuName = "Events/String Event Channel")]
    public class StringEventChannelSO : ScriptableObject
    {
        public UnityAction<string> OnEventRaised;

        /// <summary>
        /// Raise an event in this channel.
        /// Any Listeners listening to this channel will be notified.
        /// </summary>
        public void RaiseStringEvent(string message)
        {
            OnEventRaised?.Invoke(message);
        }
    }
}