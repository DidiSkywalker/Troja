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

        public void SetActiveMinigame(MinigameSO minigame)
        {
            MinigameState.Instance.ActiveMinigame = minigame;
        }

        public void UnsetActiveMinigame()
        {
            MinigameState.Instance.ActiveMinigame = null;
        }
    }
}