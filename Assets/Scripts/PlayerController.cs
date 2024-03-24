using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRB;
    public float speed;
    public float jumpForce;
    public bool canJump;

    public Transform orientation;

    public float horizontalInput;
    public float verticalInput;
    public Vector3 moveDirection;
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRB.freezeRotation = true;
        speed = 5;
        jumpForce = 13f;
        canJump = true;
    }
    void Update()
    {
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
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void PlayerMovement()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        playerRB.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
    }

    private void LimitSpeed()
    {
        Vector3 flatVel = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            playerRB.velocity = new Vector3(limitedVel.x, playerRB.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //reset players y velocity for consistent jump height
        playerRB.velocity = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);
        
        playerRB.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        
        StartCoroutine(ResetJump());
        
    }

    private IEnumerator ResetJump()
    {
        canJump = false;
        yield return new WaitForSeconds(1.8f); //jump cd
        canJump = true;
    }
}
