using Events.Channels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minigames
{
    /// <summary>
    /// This class handles loading and unloading of Minigame scenes as they're launched, aborted or successfully
    /// completed.
    /// The MinigameSceneLoader relies on up-to-date state in MinigameState and itself raises an event
    /// in the MinigameSceneUnloadedEventChannel, to allow the MinigameStateManager to clean up its currently active
    /// minigame after the scene unloaded.
    /// </summary>
    public class MinigameSceneLoader : MonoBehaviour
    {

        public VoidEventChannelSO minigameSceneUnloadedEventChannel;

        /// <summary>
        /// Additively load the scene of the given minigame into the game.
        /// </summary>
        /// <param name="minigame">The minigame to load</param>
        public void LoadMinigameScene(MinigameSO minigame)
        {
            CityScreenshotHelper.SaveTexture();
            SceneManager.LoadSceneAsync(minigame.minigameScene.ScenePath, LoadSceneMode.Additive);
        }

        /// <summary>
        /// Unload the currently active minigame.
        /// The currently active minigame is whatever is stored in MinigameState.
        /// 
        /// Raises an event in the MinigameSceneUnloadedEventChannel.
        /// </summary>
        public void UnloadMinigameScene()
        {
            SceneManager.UnloadSceneAsync(MinigameState.Instance.ActiveMinigame.minigameScene.ScenePath);
            minigameSceneUnloadedEventChannel.RaiseEvent();
        }
    }
}
