using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CompanionInteraction : MonoBehaviour
{
    public Transform player;
    public float interactDistance = 2f;

    public GameObject dialogCanvas;
    public Text dialogText;
    public GameObject yesButton;
    public GameObject noButton;

    private bool isInRange;
    private bool hasInteracted;
    private bool hasPaid;

    private TrashCounterUI trashCounter;
    private CompanionAI companionAI;

    void Start()
    {
        dialogCanvas.SetActive(false);
        trashCounter = FindObjectOfType<TrashCounterUI>();
        companionAI = GetComponent<CompanionAI>();
        if (companionAI != null)
            companionAI.enabled = false; // Companion tidak aktif dulu
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        isInRange = distance <= interactDistance;

        if (isInRange && !hasInteracted && Input.GetKeyDown(KeyCode.F))
        {
            ShowDialog();
        }

        if (dialogCanvas.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                if (trashCounter.GetTrashCount() >= 1)
                {
                    trashCounter.RemoveTrash(1);
                    dialogText.text = "Terima kasih! Aku akan membantumu.";
                    hasPaid = true;
                    hasInteracted = true;
                    StartCoroutine(CloseDialogAfterDelay(1.5f));
                }
                else
                {
                    dialogText.text = "Kamu tidak punya cukup Sampah!";
                    StartCoroutine(CloseDialogAfterDelay(1.5f));
                }
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                dialogText.text = "Baik, mungkin nanti.";
                hasPaid = false;
                StartCoroutine(CloseDialogAfterDelay(1.5f));
            }
        }
    }

    void ShowDialog()
    {
        Time.timeScale = 0f; // Pause game
        dialogCanvas.SetActive(true);
        dialogText.text = "Hei, aku bisa bantu kamu. Bayar 1 Sampah? (Y/N)";
        yesButton.SetActive(true);
        noButton.SetActive(true);
    }

    IEnumerator CloseDialogAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        CloseDialog();
    }

    void CloseDialog()
    {
        Time.timeScale = 1f; // Resume game
        dialogCanvas.SetActive(false);

        if (hasPaid && companionAI != null)
        {
            companionAI.enabled = true; // Companion mulai bantu
        }
    }
}
