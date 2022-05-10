using UnityEngine;

namespace Minigames
{
    /// <summary>
    /// Holds state on the currently active minigame.
    /// </summary>
    [CreateAssetMenu(menuName = "Minigames/Minigame State")]
    public class MinigameStateSO : ScriptableObject
    {
        /// <summary>
        /// The currently active minigame.
        /// Can be null.
        /// </summary>
        public MinigameSO activeMinigame;

    }
}