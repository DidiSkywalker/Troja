using Minigames;
using UnityEngine;
using UnityEngine.Events;

namespace Events.Channels
{
    /// <summary>
    /// This class implements a channel for events with a parameter of type PageSO
    /// </summary>

    [CreateAssetMenu(menuName = "Events/Book Event Channel")]
    public class BookEventChannelSO : ScriptableObject
    {
        public UnityAction<PageSO> OnEventRaised;

        /// <summary>
        /// Raise an event in this channel.
        /// Any BookEventListeners listening to this channel will be notified.
        /// </summary>
        public void RaiseEvent(PageSO page)
        {
            OnEventRaised?.Invoke(page);
        }
    }
}