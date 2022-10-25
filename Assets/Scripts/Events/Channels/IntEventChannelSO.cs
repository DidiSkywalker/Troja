using UnityEngine;
using UnityEngine.Events;

namespace Events.Channels
{
    /// <summary>
    /// This class implements a channel for events with a parameter of type int
    /// </summary>
    [CreateAssetMenu(menuName = "Events/Int Event Channel")]
    public class IntEventChannelSO : ScriptableObject
    {
        public UnityAction<int> OnEventRaised;

        /// <summary>
        /// Raise an event in this channel.
        /// Any Listeners listening to this channel will be notified.
        /// </summary>
        public void RaiseStringEvent(int message)
        {
            OnEventRaised?.Invoke(message);
        }
    }
}