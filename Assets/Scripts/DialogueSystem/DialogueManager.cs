using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] private GameObject dialogueObject;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;
    }

    public void ShowDialogue(Conversation conversation)
    {
        dialogueObject.SetActive(true);
        dialogueText.text = conversation.nodes[0].text;
        speakerText.text = conversation.nodes[0].speakerName;
    }
}
