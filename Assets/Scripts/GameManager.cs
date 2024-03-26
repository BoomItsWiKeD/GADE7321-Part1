using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerScore;
    public TMP_Text playerScoreText;
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
        //initialise game
        Time.timeScale = 1;
        playerScore = 0;
        opponentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //pause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //collects the blue flag
        if (other.gameObject.CompareTag("BlueFlag"))
        {
            blueFlag.SetActive(false);
            carryingBlue = true;
            carryingBlueText.SetActive(true);
        }

        //deposits blue flag
        if (other.gameObject.CompareTag("BlueFlagHome") && carryingBlue == true)
        {
            carryingBlue = false;
            blueFlag.SetActive(true);
            playerScore = playerScore + 1;
            playerScoreText.text = "Your Score: " + playerScore; //updates player score UI
            carryingBlueText.SetActive(false);
            if (playerScore == 5) //ends game when player wins
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
