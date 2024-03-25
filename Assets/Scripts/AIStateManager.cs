using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Hardware;
using UnityEngine;
using UnityEngine.AI;

public class AIStateManager : MonoBehaviour
{
    public GameObject redFlag;
    public GameObject redFlagHome;
    public GameObject redFlagText;
    public GameObject defeatScreen;

    public bool carryingRed;
    public int enemyScore;
    
    public NavMeshAgent agent;
    public Transform target;
    public Transform flagPos;
    public Transform homePos;
    public Transform powerUpPos;
    public string currentState;
    

    
    void Start()
    {
        Time.timeScale = 1;
        enemyScore = 0;
        carryingRed = false;
        currentState = "FetchState";
        flagPos = GameObject.FindWithTag("RedFlag").transform;
        homePos = GameObject.FindWithTag("RedFlagHome").transform;
        agent.SetDestination(flagPos.transform.position);
    }
    void Update()
    {
        /*if (currentState == "FetchState")
        {
            agent.SetDestination(flagPos.transform.position);
        }
        if (currentState == "ReturnState")
        {
            agent.SetDestination(homePos.transform.position);
        }

        if (currentState == "PowerUpState")
        {
            agent.SetDestination(powerUpPos.transform.position);
        }*/
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RedFlag"))
        {
            agent.SetDestination(homePos.transform.position);
            carryingRed = true;
            redFlag.SetActive(false);
            redFlagText.SetActive(true);
            currentState = "Return State";
        }

        if (other.gameObject.CompareTag("RedFlagHome") && carryingRed == true)
        {
            agent.SetDestination(flagPos.transform.position);
            carryingRed = false;
            redFlag.SetActive(true);
            redFlagText.SetActive(false);
            currentState = "FetchState";
            enemyScore = enemyScore + 1;
            if (enemyScore == 5)
            {
                Defeat();
            }
        }
    }

    public void Defeat()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        defeatScreen.SetActive(true);
    }
}
