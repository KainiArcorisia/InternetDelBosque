using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialoguePoint dialogue;
    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            DialogueManager.Instance.EndDialogue();
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.H))
        {
            DialogueManager.Instance.StartDialogue(dialogue);
        }
    }
}