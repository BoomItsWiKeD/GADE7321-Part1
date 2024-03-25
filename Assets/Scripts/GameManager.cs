using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerScore;
    public int opponentScore;

    public GameObject blueFlag;
    public GameObject redFlag;

    public bool carryingBlue;
    public GameObject carryingBlueText;
    public bool carryingRed;
    public GameObject carryingRedText;

    public GameObject victoryScreen;
    public GameObject pauseScreen;
    void Start()
    {
        Time.timeScale = 1;
        playerScore = 0;
        opponentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BlueFlag"))
        {
            blueFlag.SetActive(false);
            carryingBlue = true;
            carryingBlueText.SetActive(true);
        }

        if (other.gameObject.CompareTag("BlueFlagHome") && carryingBlue == true)
        {
            carryingBlue = false;
            blueFlag.SetActive(true);
            playerScore = playerScore + 1;
            carryingBlueText.SetActive(false);
            if (playerScore == 5)
            {
                Victory();
            }
        }
    }

    private void Victory()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        victoryScreen.SetActive(true);
    }

    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }
    
}
