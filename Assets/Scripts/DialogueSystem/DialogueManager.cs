using System.Collections;
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

    private IEnumerator WriteText(string dialogueText)
    {
        char[] textArr = dialogueText.ToCharArray();
        text.text = "";

        while (text.text.Length < dialogueText.Length)
        {
            text.text += textArr[text.text.Length];
            yield return new WaitForSeconds(typingDelay);
        }
        dialogueIndex++;
        writingCoroutine = null;
    }

    private void Interact(PlayerCore player)
    {
        if (currentDialogue == null) { return; }

        if (writingCoroutine != null) // if the current section of dialoge has not finished getting written
        {
            StopCoroutine(writingCoroutine);
            writingCoroutine = null;
            text.text = currentDialogue.parts[dialogueIndex];
            dialogueIndex++;
        }
        else if (dialogueIndex < currentDialogue.parts.Length) // otherwise, if there are more sections of dialogue to be written
        {
            writingCoroutine = StartCoroutine(WriteText(currentDialogue.parts[dialogueIndex]));
        }
        else // otherwise, if the dialogue is done
        {
            if (bgAnimationCoroutine != null)
            {
                StopCoroutine(bgAnimationCoroutine);
                bgAnimationCoroutine = null;
            }

            finishCallback?.Invoke(null);
            UIManager.OnUISelect -= Interact;
            PlayerManager.Instance.ChangeInputMapAll("Player");
            Destroy(gameObject);
        }
    }
}
