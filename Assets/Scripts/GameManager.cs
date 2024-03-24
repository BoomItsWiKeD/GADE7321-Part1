using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerScore;
    public int opponentScore;

    public GameObject blueFlag;
    public GameObject redFlag;

    public bool carryingBlue;
    public bool carryingRed;
    void Start()
    {
        playerScore = 0;
        opponentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BlueFlag"))
        {
            blueFlag.SetActive(false);
            carryingBlue = true;
        }

        if (other.gameObject.CompareTag("BlueFlagHome") && carryingBlue == true)
        {
            carryingBlue = false;
            blueFlag.SetActive(true);
            playerScore = playerScore + 1;
            if (playerScore == 5)
            {
                Victory();
            }
        }
    }

    private void Victory()
    {
        
    }
    
}
