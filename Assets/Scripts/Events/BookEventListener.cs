using Events.Channels;
using Page;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    /// <summary>
    /// This component adds a listener to ScriptableObject Assets of type BookEventChannelSO
    /// to a GameObject. This allows the GameObject to react to events in this channel.
    /// </summary>
    public class BookEventListener : MonoBehaviour
    {
        [SerializeField] private BookEventChannelSO channel;
        public UnityEvent<PageSO> onEventRaised;

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

        private void Respond(PageSO page)
        {
            onEventRaised?.Invoke(page);
        }
    }
}