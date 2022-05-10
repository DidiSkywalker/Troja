using UnityEngine;

namespace Minigames
{
    /// <summary>
    /// Provides a way to keep an MinigameStateSO instance up-to-date.
    /// To do this attach listeners for relevant events to the same GameObject
    /// and call the provided methods accordingly.
    /// </summary>
    public class MinigameStateManager : MonoBehaviour
    {
        public MinigameStateSO minigameState;

        public void SetActiveMinigame(MinigameSO minigame)
        {
            minigameState.activeMinigame = minigame;
        }

        public void UnsetActiveMinigame()
        {
            minigameState.activeMinigame = null;
        }
    }
}