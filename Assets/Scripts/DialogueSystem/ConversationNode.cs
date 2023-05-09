using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConversationNode
{
    public NodeType nodeType;
    public string speakerName;
    [TextArea] public string text;
    public ConversationButton[] buttons;
}

public enum NodeType
{
    Normal,
    Thinking,
    Surprise
}