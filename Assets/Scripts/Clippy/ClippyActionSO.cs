using UnityEngine;

namespace ClippyAction
{
    /// <summary>
    /// Describes an action of clippy as an Asset.
    /// This holds a reference to the movement or speech of clippy
    /// </summary>
    [CreateAssetMenu(menuName = "ClippyAction/ClippyAction")]
    public class ClippyActionSO : ScriptableObject
    {
        public string clippyActionName;
        public bool doesSpeak, doesEnter, doesExit, doesMove;
        public string[] says;
    }
}