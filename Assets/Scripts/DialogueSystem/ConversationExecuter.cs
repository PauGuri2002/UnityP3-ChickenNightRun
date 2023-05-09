using UnityEngine;

public class ConversationExecuter : MonoBehaviour
{
    [SerializeField] private Conversation conversation;

    public void ExecuteConversation()
    {
        DialogueManager.instance.ShowDialogue(conversation);
    }
}
