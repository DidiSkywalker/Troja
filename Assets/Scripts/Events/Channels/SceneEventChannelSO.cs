using DevLocker.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Events.Channels
{
    /// <summary>
    /// This class implements a channel for events with a parameter of type SceneReference
    /// </summary>
    [CreateAssetMenu(menuName = "Events/Scene Event Channel")]
    public class SceneEventChannelSO : ScriptableObject
    {
        public UnityAction<SceneReference> OnEventRaised;

        /// <summary>
        /// 
        /// Raise an event in this channel.
        /// Any SceneEventListeners listening to this channel will be notified.
        /// </summary>
        public void RaiseEvent(SceneReference scene)
        {
            OnEventRaised?.Invoke(scene);
        }

    }
}