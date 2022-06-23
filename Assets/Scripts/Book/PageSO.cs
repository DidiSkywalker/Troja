using DevLocker.Utils;
using UnityEngine;

namespace Minigames
{
    /// <summary>
    /// Describes a page as an Asset.
    /// This holds a reference to the page text, as well as a reference to the 3d model
    /// </summary>
    [CreateAssetMenu(menuName = "Page/PageSO")]
    public class PageSO : ScriptableObject
    {
        public string pageText;
        public Mesh artifactMesh;
        public Texture artifactTexture;
    }
}