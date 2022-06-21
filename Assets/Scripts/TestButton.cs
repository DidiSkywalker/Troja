using Events.Channels;
using Enums;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public string text;
    public StringEventChannelSO sayEventChannel;
    public MovementEventChannelSO movementEventChannel;

    public void OnButtonClick()
    {
        movementEventChannel.RaiseMovementEvent(AnimatedMovements.SLIDEIN);
        sayEventChannel.RaiseStringEvent(text);
        print("test button clicked");
    }
}
