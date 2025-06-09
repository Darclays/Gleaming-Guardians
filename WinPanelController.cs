using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanelController : MonoBehaviour
{
    public GameObject winPanel; // Assign via Inspector

    // Panggil fungsi ini saat player menang
    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f; // Pause game
    }

    // Fungsi untuk tombol Main Menu
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Resume game
        SceneManager.LoadScene("MainMenu"); // Pastikan nama scene MainMenu benar
    }
}