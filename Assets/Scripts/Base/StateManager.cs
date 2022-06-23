using DevLocker.Utils;
using Minigames;
using UnityEngine;

namespace Base
{
    public class StateManager : MonoBehaviour
    {
        public void SetActiveMinigame(MinigameSO minigame)
        {
            State.Instance.ActiveMinigame = minigame;
        }

        public void UnsetActiveMinigame()
        {
            State.Instance.ActiveMinigame = null;
        }

        public void SetActiveLevel(SceneReference level)
        {
            State.Instance.ActiveLevel = level;
        }

        public void UnsetActiveLevel()
        {
            State.Instance.ActiveLevel = default;
        }
    }
}