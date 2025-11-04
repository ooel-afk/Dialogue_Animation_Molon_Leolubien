using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public TMP_Text dialogueText;
    public float typingSpeed = 0.05f;

    [TextArea(3, 10)]
    public string[] dialogueLines;

    private int currentLine = 0;
    private bool isTyping = false;

    void Start()
    {
        if (dialogueLines.Length > 0)
        {
            StartCoroutine(TypeLine(dialogueLines[currentLine]));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[currentLine];
                isTyping = false;
            }
            else
            {
                ShowNextLine();
            }
        }
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = ""; // Clear the text first

        // Add one letter at a time
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void ShowNextLine()
    {
        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            StartCoroutine(TypeLine(dialogueLines[currentLine]));
        }
        else
        {
            dialogueText.text = "";
            Debug.Log("All dialogue finished!");
        }
    }
}
