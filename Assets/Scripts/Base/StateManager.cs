using DevLocker.Utils;
using Minigames;
using UnityEngine;
using UnityEngine.UI;

namespace Base
{
    public class StateManager : MonoBehaviour
    {
        public Button menuButton;
        
        public void SetActiveMinigame(MinigameSO minigame)
        {
            menuButton.gameObject.SetActive(false);
            State.Instance.ActiveMinigame = minigame;
        }

        public void UnsetActiveMinigame()
        {
            State.Instance.ActiveMinigame = null;
        }

        public void SetActiveLevel(SceneReference level)
        {
            menuButton.gameObject.SetActive(true);
            State.Instance.ActiveLevel = level;
        }

        public void UnsetActiveLevel()
        {
            print("Unset active level");
            menuButton.gameObject.SetActive(false);
            State.Instance.ActiveLevel = default;
        }
    }
}