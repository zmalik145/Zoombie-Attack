using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public Button pauseButton;
    public Button resumeButton;
    public Button restartButton;
    public Text levelCompleted;
    public Text gameOver;
    public Text pausedText;

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        pauseButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        Time.timeScale = 0f;
        pausedText.gameObject.SetActive(true);
    }
    public void Resume()
    {
        pauseButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);
        Time.timeScale = 1f;
        pausedText.gameObject.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
