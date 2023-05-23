using UnityEngine.Events;
using static DialogueEventManager;

[System.Serializable]
public class DialogueEvent
{
    public ChickenEvent eventId;
    public UnityEvent onEventCall;
}
