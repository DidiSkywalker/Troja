using DevLocker.Utils;
using Events.Channels;
using UnityEngine;

namespace MainMenu
{
    public class Book : MonoBehaviour
    {
        public SceneReference levelScene;
        public SceneEventChannelSO loadLevelEventChannel;
        public float hoverOffset = -0.05f;

        private void OnMouseEnter()
        {
            gameObject.transform.position += new Vector3(0, 0, hoverOffset);
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0) && !levelScene.IsEmpty)
            {
                loadLevelEventChannel.RaiseEvent(levelScene);
            }
        }

        private void OnMouseExit()
        {
            gameObject.transform.position -= new Vector3(0, 0, hoverOffset);
        }
    }
}