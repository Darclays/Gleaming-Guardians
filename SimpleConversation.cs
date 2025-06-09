using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleConversation : MonoBehaviour
{
    public Transform player;
    public float interactDistance = 2f;

    public GameObject dialogCanvas;
    public Text dialogText;

    [TextArea(2, 5)]
    public string[] dialogLines;

    private int currentLine = 0;
    private bool isInRange;
    private bool isTalking = false;
    private bool hasTalked = false;

    void Start()
    {
        dialogCanvas.SetActive(false);
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        isInRange = distance <= interactDistance;

        if (isInRange && !hasTalked && Input.GetKeyDown(KeyCode.F))
        {
            StartConversation();
        }
        else if (isTalking && Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextLine();
        }
    }

    void StartConversation()
    {
        Time.timeScale = 0f; // Pause gameplay
        dialogCanvas.SetActive(true);
        dialogText.text = dialogLines[0];
        currentLine = 0;
        isTalking = true;
    }

    void ShowNextLine()
    {
        currentLine++;
        if (currentLine < dialogLines.Length)
        {
            dialogText.text = dialogLines[currentLine];
        }
        else
        {
            EndConversation();
        }
    }

    void EndConversation()
    {
        dialogCanvas.SetActive(false);
        Time.timeScale = 1f; // Resume gameplay
        isTalking = false;
        hasTalked = true;
    }
}
