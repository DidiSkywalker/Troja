using DevLocker.Utils;
using Events.Channels;
using Minigames;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base
{
    public class SceneLoader : MonoBehaviour
    {
        public SceneReference mainMenuScene;
        public VoidEventChannelSO minigameSceneUnloadedEventChannel;
        
        private string _currentlyLoadingScene;
        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
        
        public void LoadMinigameScene(MinigameSO minigame)
        {
            var activeScene = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(activeScene);
            _currentlyLoadingScene = minigame.minigameScene.SceneName;
            SceneManager.LoadSceneAsync(minigame.minigameScene.ScenePath, LoadSceneMode.Additive);
        }

        public void LoadLevelScene(SceneReference level)
        {
            _currentlyLoadingScene = level.SceneName;
            SceneManager.LoadSceneAsync(level.SceneName, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(mainMenuScene.SceneName);
        }

        public void LoadMenu()
        {
            var activeScene = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(activeScene);
            _currentlyLoadingScene = mainMenuScene.SceneName;
            SceneManager.LoadSceneAsync(mainMenuScene.ScenePath, LoadSceneMode.Additive);
        }

        public void UnloadMinigameScene(MinigameSO minigame)
        {
            SceneManager.UnloadSceneAsync(minigame.minigameScene.SceneName);
            minigameSceneUnloadedEventChannel.RaiseEvent();
            _currentlyLoadingScene = State.Instance.ActiveLevel.SceneName;
            SceneManager.LoadSceneAsync(State.Instance.ActiveLevel.ScenePath, LoadSceneMode.Additive);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == _currentlyLoadingScene)
            {
                SceneManager.SetActiveScene(scene);
                _currentlyLoadingScene = null;
            }
        }
        
        private void OnSceneUnloaded(Scene scene)
        {
            
        }
    }
}