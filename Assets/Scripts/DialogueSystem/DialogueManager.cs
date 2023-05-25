using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject skipButton;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private float typingDelay = 0.1f;
    [SerializeField] private DialogueEventManager dialogueEventManager;
    private Conversation currentConversation;
    private int dialogueIndex;
    private List<GameObject> createdButtons = new List<GameObject>();
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
        if(currentConversation != null) { return; }

        Cursor.lockState = CursorLockMode.None;
        dialogueObject.SetActive(true);
        currentConversation = conversation;
        dialogueIndex = 0;
        ShowDialogueNode(conversation.nodes[0]);
    }

    public void HideDialogue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentConversation = null;
        dialogueObject.SetActive(false);
    }

    public void ShowDialogueNode(ConversationNode node)
    {
        // speaker
        speakerText.text = node.speakerName;

        // destroy buttons
        foreach (GameObject button in createdButtons)
        {

            Destroy(button);
        }
        createdButtons.Clear();

        // text
        if(writingCoroutine != null)
        {
            StopCoroutine(writingCoroutine);
        }
        writingCoroutine = StartCoroutine(WriteText(node.text));

    }

    public void ShowDialogueButtons(ConversationNode node)
    {
        skipButton.SetActive(false);

        if (node.buttons.Length > 0)
        {
            foreach (ConversationButton button in node.buttons)
            {
                GameObject buttonInstance = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);
                Button buttonComponent = buttonInstance.GetComponent<Button>();
                if (button.targetNodeIndex < 0)
                {
                    buttonComponent.onClick.AddListener(HideDialogue);
                }
                else
                {
                    buttonComponent.onClick.AddListener(() => ShowDialogueNode(currentConversation.nodes[button.targetNodeIndex]));
                }

                if (button.onClickEvent != DialogueEventManager.ChickenEvent.None)
                {
                    buttonComponent.onClick.AddListener(() => dialogueEventManager.FireDialogueEvent(button.onClickEvent));
                }

                TextMeshProUGUI buttonText = buttonInstance.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = button.text;

                buttonInstance.transform.SetParent(buttonsContainer.transform, false);
                createdButtons.Add(buttonInstance);
            }
        }
    }

    private IEnumerator WriteText(string dialogueString)
    {
        char[] textArr = dialogueString.ToCharArray();
        dialogueText.text = "";
        skipButton.SetActive(true);

        while (dialogueText.text.Length < dialogueString.Length)
        {
            dialogueText.text += textArr[dialogueText.text.Length];
            yield return new WaitForSeconds(typingDelay);
        }
        writingCoroutine = null;
        ShowDialogueButtons(currentConversation.nodes[dialogueIndex]);
        dialogueIndex++;
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
            ShowDialogueButtons(currentConversation.nodes[dialogueIndex]);
            dialogueIndex++;
        }
    }
}
