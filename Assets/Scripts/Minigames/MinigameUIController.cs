using Events.Channels;
using UnityEngine;
using UnityEngine.UIElements;

namespace Minigames
{
    /// <summary>
    /// Controller for the minigame overlay UI.
    /// Handles showing and hiding the UI, propagating button clicks, etc.
    /// </summary>
    public class MinigameUIController : MonoBehaviour
    {
        public UIDocument uiDocument;
        public bool visibleByDefault;
        public VoidEventChannelSO minigameUIExitEventChannel;

        private void Start()
        {
            if (visibleByDefault)
            {
                SetVisible();
            }
            else
            {
                SetInvisible();
            }

            InitControls();
        }

        public void SetVisible()
        {
            uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        public void SetInvisible()
        {
            uiDocument.rootVisualElement.style.display = DisplayStyle.None;
        }

        private void InitControls()
        {
            var root = uiDocument.rootVisualElement;
            var exitButton = root.Q<Button>("exit");
            exitButton.clickable.clicked += () =>
            {
                SetInvisible();
                minigameUIExitEventChannel.RaiseEvent();
            };
        }
    }
}