using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public GameObject pauseScreen;
    public void OnPlayAgainClick()
    {
        SceneManager.LoadScene("CTF Mode");
    }

    public void OnResumeClick()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
    
}
