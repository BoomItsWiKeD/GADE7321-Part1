using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRB;
    public float speed;
    public float speedMulti;
    public float jumpForce;
    public float jumpMulti;
    public bool canJump;

    public GameObject jumpPowerUp;
    public GameObject speedPowerUp;
    public GameObject speedPowerUp2;
    
    public Transform orientation;

    public float horizontalInput;
    public float verticalInput;
    public Vector3 moveDirection;
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRB.freezeRotation = true;
        speed = 5f;
        speedMulti = 1f;
        jumpForce = 13f;
        jumpMulti = 1f;
        canJump = true;
    }
    void Update()
    {
        //jumps when jump cooldown has ended
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            Jump();
        }
        
        PlayerInput();
        LimitSpeed();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerInput()
    {
        //checks player inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void PlayerMovement()
    {
        //adds forces to player based on which direction the player is facing
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        playerRB.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
    }

    private void LimitSpeed() //limits the player to only reach certain speeds
    {
        Vector3 flatVel = new Vector3(playerRB.velocity.x * speedMulti, 0f, playerRB.velocity.z * speedMulti);

        if (flatVel.magnitude > speed) //slows the player down when they have reached max speed
        {
            Vector3 limitedVel = flatVel.normalized * speed * speedMulti;
            playerRB.velocity = new Vector3(limitedVel.x, playerRB.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //reset players y velocity for consistent jump height
        playerRB.velocity = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);
        
        playerRB.AddForce(transform.up * jumpForce * jumpMulti, ForceMode.Impulse);
        
        StartCoroutine(ResetJump()); //starts jump cooldown
    }

    private IEnumerator ResetJump()
    {
        canJump = false;
        yield return new WaitForSeconds(1.8f); //jump cd
        canJump = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        //checks which power up the player collected
        if (other.gameObject.CompareTag("JumpPowerUp"))
        {
            StartCoroutine(JumpPowerUpDuration());
        }

        if (other.gameObject.CompareTag("SpeedPowerUp"))
        {
            StartCoroutine(SpeedPowerUpDuration());
        }
        
        if (other.gameObject.CompareTag("SpeedPowerUp2"))
        {
            StartCoroutine(SpeedPowerUpDuration2());
        }
    }
    
    //gives the correct effects based on which power up is collected and respawns power-up when the effect ends
    private IEnumerator JumpPowerUpDuration()
    {
        jumpPowerUp.SetActive(false);
        jumpMulti = 2f;
        yield return new WaitForSeconds(6f);
        jumpMulti = 1f;
        jumpPowerUp.SetActive(true);
    }

    private IEnumerator SpeedPowerUpDuration()
    {
        speedPowerUp.SetActive(false);
        speedMulti = 1.5f;
        yield return new WaitForSeconds(6f);
        speedMulti = 1f;
        speedPowerUp.SetActive(true);
    }
    private IEnumerator SpeedPowerUpDuration2()
    {
        speedPowerUp2.SetActive(false);
        speedMulti = 1.5f;
        yield return new WaitForSeconds(6f);
        speedMulti = 1f;
        speedPowerUp2.SetActive(true);
    }
}
