using DevLocker.Utils;
using Minigames;

namespace Base
{
    public class State
    {
        public static readonly State Instance = new();

        public static bool IsMinigameRunning()
        {
            return Instance.ActiveMinigame != null;
        }

        public MinigameSO ActiveMinigame;
        public MinigameParams MinigameParams;
        public SceneReference ActiveLevel;
        private State() {}

        public T GetParams<T>() where T : MinigameParams
        {
            if (MinigameParams is T @params)
            {
                return @params;
            }

            return null;
        }
    }
}