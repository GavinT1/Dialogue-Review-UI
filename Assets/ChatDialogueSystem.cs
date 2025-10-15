using UnityEngine;
using TMPro;

[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3, 10)]
    public string[] texts;
}

public class ChatDialogueSystem : MonoBehaviour
{
    public Dialogue[] dialogues;
    public GameObject chatPrefab;
    public Transform chatcontent;
    public float delayBetweenMessages = 0.5f;

    private int dialogueIndex = 0;     // which character (John, Carl, etc.)
    private int textIndex = 0;         // which line within that character
    private bool isChatting = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isChatting)
            {
                StartChat();
            }
            else
            {
                DisplayNextMessage();
            }
        }
    }

    void StartChat()
    {
        isChatting = true;
        dialogueIndex = 0;
        textIndex = 0;

        AddChatMessage(dialogues[dialogueIndex].name, dialogues[dialogueIndex].texts[textIndex]);
    }

    void DisplayNextMessage()
    {
        textIndex++;

        // Move to next dialogue group if done with current
        if (textIndex >= dialogues[dialogueIndex].texts.Length)
        {
            dialogueIndex++;
            textIndex = 0;
        }

        // End if all dialogues are done
        if (dialogueIndex >= dialogues.Length)
        {
            isChatting = false;
            return;
        }

        AddChatMessage(dialogues[dialogueIndex].name, dialogues[dialogueIndex].texts[textIndex]);
    }

    void AddChatMessage(string speaker, string message)
    {
        GameObject newMsg = Instantiate(chatPrefab, chatcontent);
        TextMeshProUGUI msgText = newMsg.GetComponentInChildren<TextMeshProUGUI>();

        if (msgText != null)
        {
            msgText.text = $"<b>{speaker}:</b> {message}";
        }
        else
        {
            Debug.LogWarning("No TextMeshProUGUI found inside Chat Prefab!");
        }
    }
}