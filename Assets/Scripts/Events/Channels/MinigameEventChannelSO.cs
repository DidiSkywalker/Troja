using Base;
using Minigames;
using UnityEngine;
using UnityEngine.Events;

namespace Events.Channels
{
    /// <summary>
    /// This class implements a channel for events with a parameter of type MinigameSO
    /// </summary>
    
    [CreateAssetMenu(menuName = "Events/Minigame Event Channel")]
    public class MinigameEventChannelSO : ScriptableObject
    {
        
        public UnityAction<MinigameSO> OnEventRaised;

        /// <summary>
        /// 
        /// Raise an event in this channel.
        /// Any MinigameEventListeners listening to this channel will be notified.
        /// </summary>
        public void RaiseEvent(MinigameSO minigame)
        {
            OnEventRaised?.Invoke(minigame);
        }

        public void RaiseEvent(MinigameSO minigame, MinigameParams minigameParams)
        {
            if (OnEventRaised != null)
            {
                State.Instance.MinigameParams = minigameParams;
                OnEventRaised.Invoke(minigame);
            }
        }
    }
}