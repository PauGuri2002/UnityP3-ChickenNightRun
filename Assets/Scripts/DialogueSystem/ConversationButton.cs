using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static DialogueEventManager;

[System.Serializable]
public class ConversationButton
{
    public string text;
    public int targetNodeIndex;
    public ChickenEvent onClickEvent;
}
