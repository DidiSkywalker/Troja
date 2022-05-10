using DevLocker.Utils;
using UnityEngine;

namespace Minigames
{
    /// <summary>
    /// Describes a minigame as an Asset.
    /// This holds a reference to the minigames scene, as well as other information
    /// the minigames might need in the future. (I'm thinking settings, what artifact they unlock, what
    /// building/district launched the game, etc)
    /// </summary>
    [CreateAssetMenu(menuName = "Minigames/Minigame")]
    public class MinigameSO : ScriptableObject
    {
        public SceneReference minigameScene;
        public string minigameName;
    }
}