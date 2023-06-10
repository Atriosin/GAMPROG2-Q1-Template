using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.0f;
    [SerializeField]
    private float gravityScale = 1.0f;

    [SerializeField]
    private float playerHeight;
    [SerializeField]
    private float jumpPower = 1.0f;

    private Vector3 playerVelocity;
    public LayerMask whatIsGround;

    [SerializeField]
    private bool grounded;

    bool readyToJump;

    private float gravity = -9.8f;
    private float negative = -1.0f;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        readyToJump = true;
    }

    private void Update()
    {
        Move();
        Debug.Log(grounded);
    }

    private void Move()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        Vector3 moveDirection = (transform.right * xMove) + (transform.forward * zMove);
        moveDirection.y += gravity * Time.deltaTime * gravityScale;
        moveDirection *= moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            Debug.Log("We jumped homie.");
            readyToJump = false;

            Jump();
        }
        
        if (grounded)
        {
            readyToJump = true;
        }

        //Debug.Log(moveDirection);
        characterController.Move(moveDirection);
    }

    private void Jump()
    {
        playerVelocity.y += Mathf.Sqrt(jumpPower * negative * gravity);
        readyToJump = false;
        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

}
