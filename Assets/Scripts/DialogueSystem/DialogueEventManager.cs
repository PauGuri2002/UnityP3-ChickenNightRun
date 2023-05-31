using System;
using UnityEngine;
using UnityEngine.Events;

public class DialogueEventManager : MonoBehaviour
{
    [SerializeField] private DialogueEvent[] eventList;

    public void FireDialogueEvent(ChickenEvent eventId)
    {
        UnityEvent e = Array.Find(eventList, (elem) => elem.eventId == eventId).onEventCall;
        e?.Invoke();
    }

    public enum ChickenEvent
    {
        None,
        GetHitEvent,
        ResetEffectsEvent,
        OldTimesEvent,
        DiscoEvent,
        DrunkEvent,
        GetUmbrellaEvent
    }
}
