using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] private chickenControl chickenControl;
    [SerializeField] private GameObject dialogueObject;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject buttonsContainer;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private float typingDelay = 0.1f;
    [SerializeField] private DialogueEventManager dialogueEventManager;
    private Conversation currentConversation;
    private int dialogueIndex;
    Coroutine writingCoroutine;

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
        currentConversation = conversation;
        dialogueIndex = 0;
        ShowDialogueNode(conversation.nodes[0]);
    }

    public void HideDialogue()
    {
        dialogueObject.SetActive(false);
    }

    public void ShowDialogueNode(ConversationNode node)
    {
        // speaker
        speakerText.text = node.speakerName;

        // buttons
        foreach(Transform child in buttonsContainer.transform)
        {
            Destroy(child.gameObject);
        }

        if(node.buttons.Length > 0)
        {
            foreach(ConversationButton button in node.buttons)
            {
                GameObject buttonInstance = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);
                Button buttonComponent = buttonInstance.GetComponent<Button>();
                if(button.targetNodeIndex < 0)
                {
                    buttonComponent.onClick.AddListener(HideDialogue);
                } else
                {
                    buttonComponent.onClick.AddListener(() => ShowDialogueNode(currentConversation.nodes[button.targetNodeIndex]));
                }

                if(button.onClickEvent != DialogueEventManager.ChickenEvent.None)
                {
                    buttonComponent.onClick.AddListener(() => dialogueEventManager.FireDialogueEvent(button.onClickEvent));
                }
                buttonInstance.transform.SetParent(buttonsContainer.transform, false);
            }
        }

        // text
        if(writingCoroutine != null)
        {
            StopCoroutine(writingCoroutine);
        }
        writingCoroutine = StartCoroutine(WriteText(node.text));

    }

    private IEnumerator WriteText(string dialogueString)
    {
        char[] textArr = dialogueString.ToCharArray();
        dialogueText.text = "";

        while (dialogueText.text.Length < dialogueString.Length)
        {
            dialogueText.text += textArr[dialogueText.text.Length];
            yield return new WaitForSeconds(typingDelay);
        }
        dialogueIndex++;
        writingCoroutine = null;
    }

    public void SkipWriting()
    {
        if(currentConversation == null)
        {
            return;
        }

        if(writingCoroutine != null)
        {
            StopCoroutine(writingCoroutine);
            dialogueText.text = currentConversation.nodes[dialogueIndex].text;
        }
    }

    //private void Interact()
    //{
    //    if (currentDialogue == null) { return; }

    //    if (writingCoroutine != null) // if the current section of dialoge has not finished getting written
    //    {
    //        StopCoroutine(writingCoroutine);
    //        writingCoroutine = null;
    //        text.text = currentDialogue.parts[dialogueIndex];
    //        dialogueIndex++;
    //    }
    //    else if (dialogueIndex < currentDialogue.parts.Length) // otherwise, if there are more sections of dialogue to be written
    //    {
    //        writingCoroutine = StartCoroutine(WriteText(currentDialogue.parts[dialogueIndex]));
    //    }
    //    else // otherwise, if the dialogue is done
    //    {
    //        if (bgAnimationCoroutine != null)
    //        {
    //            StopCoroutine(bgAnimationCoroutine);
    //            bgAnimationCoroutine = null;
    //        }

    //        finishCallback?.Invoke(null);
    //        UIManager.OnUISelect -= Interact;
    //        PlayerManager.Instance.ChangeInputMapAll("Player");
    //        Destroy(gameObject);
    //    }
    //}
}
