using Events.Channels;
using Minigames;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    /// <summary>
    /// This component adds a listener to ScriptableObject Assets of type MinigameEventChannelSO
    /// to a GameObject. This allows the GameObject to react to events in this channel.
    /// </summary>
    public class MinigameEventListener : MonoBehaviour
    {
        [SerializeField] private MinigameEventChannelSO channel;
        public UnityEvent<MinigameSO> onEventRaised;

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

        private void Respond(MinigameSO minigame)
        {
            onEventRaised?.Invoke(minigame);
        }
    }
}