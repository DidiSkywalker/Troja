using Events.Channels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minigames
{
    /// <summary>
    /// This class handles loading and unloading of Minigame scenes as they're launched, aborted or successfully
    /// completed.
    /// The MinigameSceneLoader relies on up-to-date state in the given MinigameStateSO and itself raises an event
    /// in the MinigameSceneUnloadedEventChannel, to allow the MinigameStateSO to clean up its currently active
    /// minigame after the scene unloaded.
    /// </summary>
    public class MinigameSceneLoader : MonoBehaviour
    {

        public MinigameStateSO minigameState;
        public VoidEventChannelSO minigameSceneUnloadedEventChannel;

        /// <summary>
        /// Additively load the scene of the given minigame into the game.
        /// </summary>
        /// <param name="minigame">The minigame to load</param>
        public void LoadMinigameScene(MinigameSO minigame)
        {
            SceneManager.LoadSceneAsync(minigame.minigameScene.ScenePath, LoadSceneMode.Additive);
        }
        
        /// <summary>
        /// Unload the currently active minigame.
        /// The currently active minigame is whatever is stored in the given MinigameStateSO.
        /// 
        /// Raises an event in the MinigameSceneUnloadedEventChannel.
        /// </summary>
        public void UnloadMinigameScene()
        {
            SceneManager.UnloadSceneAsync(minigameState.activeMinigame.minigameScene.ScenePath);
            minigameSceneUnloadedEventChannel.RaiseEvent();
        }
    }
}
