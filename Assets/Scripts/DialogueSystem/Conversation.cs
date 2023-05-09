using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue System/Conversation")]
public class Conversation : ScriptableObject
{
    public ConversationNode[] nodes;
}