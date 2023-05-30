using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] private chickenControl chickenControl;
    [SerializeField] private GameObject dialogueObject;
    [SerializeField] private Image dialogueContainer;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject buttonsContainer;
    [SerializeField] private GameObject skipButton;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private float typingDelay = 0.1f;
    [SerializeField] private DialogueEventManager dialogueEventManager;
    private Conversation currentConversation;
    private ConversationNode currentNode;
    private List<GameObject> createdButtons = new List<GameObject>();
    Coroutine writingCoroutine;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;
        dialogueObject.SetActive(false);
    }

    public void ShowDialogue(Conversation conversation)
    {
        if (currentConversation != null) { return; }

        chickenControl.GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        Cursor.lockState = CursorLockMode.None;
        dialogueObject.SetActive(true);
        currentConversation = conversation;
        ShowDialogueNode(conversation.nodes[0]);
    }

    public void HideDialogue()
    {
        chickenControl.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        Cursor.lockState = CursorLockMode.Locked;
        currentConversation = null;
        dialogueObject.SetActive(false);
    }

    public void ShowDialogueNode(ConversationNode node)
    {
        currentNode = node;

        // background
        Color32 bgColor = node.nodeType switch
        {
            NodeType.Thinking => new Color32(105, 187, 215, 237),
            NodeType.Surprise => new Color32(214, 125, 104, 237),
            _ => new Color32(96, 96, 96, 237),
        };
        dialogueContainer.color = bgColor;

        // speaker
        speakerText.text = node.speakerName;

        // destroy buttons
        foreach (GameObject button in createdButtons)
        {

            Destroy(button);
        }
        createdButtons.Clear();

        // text
        if (writingCoroutine != null)
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
        ShowDialogueButtons(currentNode);
    }

    public void SkipWriting()
    {
        if (currentConversation == null)
        {
            return;
        }

        if (writingCoroutine != null)
        {
            StopCoroutine(writingCoroutine);
            dialogueText.text = currentNode.text;
            ShowDialogueButtons(currentNode);
        }
    }
}
