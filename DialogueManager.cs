using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private Button[] choiceButtons;
    [SerializeField] private TextMeshProUGUI[] choiceTexts;

    private DialoguePoint currentDialogue;
    private bool isDialogueActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (isDialogueActive)
            {
                EndDialogue();
            }
        }
    }

    public void StartDialogue(DialoguePoint dialogue)
    {
        currentDialogue = dialogue;
        isDialogueActive = true;
        dialoguePanel.SetActive(true);

        UpdateDialogueUI();
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        currentDialogue = null;
    }

    private void UpdateDialogueUI()
    {
        backgroundImage.sprite = currentDialogue.backgroundImage;
        characterImage.sprite = currentDialogue.characterImage;
        titleText.text = currentDialogue.title;
        contentText.text = currentDialogue.content;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < currentDialogue.choices.Count)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceTexts[i].text = currentDialogue.choices[i].choiceText;
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        if (currentDialogue != null && choiceIndex < currentDialogue.choices.Count)
        {
            DialoguePoint nextDialogue = currentDialogue.choices[choiceIndex].nextDialogue;
            if (nextDialogue != null)
            {
                StartDialogue(nextDialogue);
            }
            else
            {
                EndDialogue();
            }
        }
    }
}