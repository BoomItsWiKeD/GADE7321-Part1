using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AIStateManager : MonoBehaviour
{
    public GameObject redFlag;
    public GameObject redFlagHome;
    public GameObject redFlagText;
    public GameObject defeatScreen;
    public GameObject jumpPowerUp;
    public GameObject speedPowerUp;
    public GameObject speedPowerUp2;

    public bool searchPowerUp;
    public bool carryingRed;
    public int enemyScore;
    public TMP_Text enemyScoreText;
    public float closestPowerUpDistance;
    public float speedPowerUpDistance;
    public float speedPowerUpDistance2;
    public float jumpPowerUpDistance;

    public NavMeshAgent agent;

    private Transform currentPos;
    private Transform target;
    private Transform flagPos;
    private Transform homePos;
    private Transform jumpPowerUpPos;
    private Transform speedPowerUpPos1;
    private Transform speedPowerUpPos2;
    private string currentState;
    

    
    void Start()
    {
        //initialise game
        Time.timeScale = 1;
        enemyScore = 0;
        carryingRed = false;
        searchPowerUp = true;
        currentState = "FetchState";
        flagPos = GameObject.FindWithTag("RedFlag").transform;
        homePos = GameObject.FindWithTag("RedFlagHome").transform;
        jumpPowerUpPos = GameObject.FindWithTag("JumpPowerUp").transform;
        speedPowerUpPos1 = GameObject.FindWithTag("SpeedPowerUp").transform;
        speedPowerUpPos2 = GameObject.FindWithTag("SpeedPowerUp2").transform;
        currentPos = GameObject.FindWithTag("Enemy").transform;
        agent.speed = 4.5f;
        agent.SetDestination(flagPos.transform.position);
    }
    void Update()
    {
        currentPos = GameObject.FindWithTag("Enemy").transform; //gets current position of AI
        if (searchPowerUp)
        {
            speedPowerUpDistance = Vector3.Distance(GameObject.FindWithTag("Enemy").transform.position, GameObject.FindWithTag("SpeedPowerUp").transform.position);
            speedPowerUpDistance2 = Vector3.Distance(GameObject.FindWithTag("Enemy").transform.position, GameObject.FindWithTag("SpeedPowerUp2").transform.position);
            jumpPowerUpDistance = Vector3.Distance(GameObject.FindWithTag("Enemy").transform.position, GameObject.FindWithTag("JumpPowerUp").transform.position);
            if (speedPowerUpDistance < 5f || speedPowerUpDistance2 < 5f || jumpPowerUpDistance < 5f) //checks if any power-ups are in range
            {
                //calculates which power up is the closest and selects the closest as target
                if (speedPowerUpDistance < speedPowerUpDistance2 && speedPowerUpDistance < jumpPowerUpDistance)
                {
                    agent.SetDestination(speedPowerUpPos1.transform.position);
                }
                else if (speedPowerUpDistance2 < speedPowerUpDistance && speedPowerUpDistance2 < jumpPowerUpDistance)
                {
                    agent.SetDestination(speedPowerUpPos2.transform.position);
                }
                else if (jumpPowerUpDistance < speedPowerUpDistance && jumpPowerUpDistance < speedPowerUpDistance2)
                {
                    agent.SetDestination(jumpPowerUpPos.transform.position);
                }
            }
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RedFlag")) //changes AI destination to deposit flag
        {
            agent.SetDestination(homePos.transform.position);
            carryingRed = true;
            redFlag.SetActive(false);
            redFlagText.SetActive(true);
            currentState = "Return State";
        }

        if (other.gameObject.CompareTag("RedFlagHome") && carryingRed == true) //goes towards the red flag to retrieve and removes red flag from AI
        {
            agent.SetDestination(flagPos.transform.position);
            carryingRed = false;
            redFlag.SetActive(true);
            redFlagText.SetActive(false);
            currentState = "FetchState";
            enemyScore = enemyScore + 1;
            enemyScoreText.text = "Enemy Score: " + enemyScore; //updates enemy score UI
            if (enemyScore == 5) //AI wins and ends game
            {
                Defeat();
            }
        }

        //running coroutines for each type of power-up
        if (other.gameObject.CompareTag("JumpPowerUp"))
        {
            StartCoroutine(JumpPowerUpEffect());
        }

        if (other.gameObject.CompareTag("SpeedPowerUp"))
        {
            StartCoroutine(SpeedPowerUpEffect());
        }

        if (other.gameObject.CompareTag("SpeedPowerUp2"))
        {
            StartCoroutine(SpeedPowerUpEffect2());
        }
        
        //chooses the destination where AI needs to go after getting any power-up
        if (other.gameObject.CompareTag("JumpPowerUp")|| other.gameObject.CompareTag("SpeedPowerUp")|| other.gameObject.CompareTag("SpeedPowerUp2")) 
        {
            
            if (carryingRed)
            {
                agent.SetDestination(homePos.transform.position);
            }

            if (!carryingRed)
            {
                agent.SetDestination(flagPos.transform.position);
            }
        }
        
    }

    //each power-up effects
    public IEnumerator JumpPowerUpEffect()
    {
        jumpPowerUp.SetActive(false);
        yield return new WaitForSeconds(6f);
        jumpPowerUp.SetActive(true);
    }

    public IEnumerator SpeedPowerUpEffect()
    {
        speedPowerUp.SetActive(false);
        agent.speed = 7.5f;
        yield return new WaitForSeconds(6f);
        agent.speed = 4.5f;
        speedPowerUp.SetActive(true);
    }

    public IEnumerator SpeedPowerUpEffect2()
    {
        speedPowerUp2.SetActive(false);
        agent.speed = 7.5f;
        yield return new WaitForSeconds(6f);
        agent.speed = 4.5f;
        speedPowerUp2.SetActive(true);
    }
    
    //end game if AI wins
    public void Defeat()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        defeatScreen.SetActive(true);
    }
}
