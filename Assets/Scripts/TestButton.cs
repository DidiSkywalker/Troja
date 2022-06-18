using Events.Channels;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public string text;
    public StringEventChannelSO sayEventChannel;

    public void OnButtonClick()
    {
        sayEventChannel.RaiseStringEvent(text);
    }
}
