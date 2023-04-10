using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerManager : MonoBehaviour
{
    public bool punching = false;

    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] float jumpSpeed = 1.0f;

    CharacterController characterController;
    Animator animator;

    Vector3 playerVel;
    Vector2 movementInput = Vector2.zero;
    bool isOnGround;

    bool jumping = false;
    float gravityValue = -9.81f;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        isOnGround = characterController.isGrounded;
        if (isOnGround && playerVel.y < 0)
        {
            playerVel.y = 0f;
        }

        Vector3 playerMoving = new Vector3(movementInput.x, 0, movementInput.y);
        characterController.Move(playerMoving * Time.deltaTime * moveSpeed);

        animator.SetBool("isRunning", false);

        if (playerMoving != Vector3.zero)
        {
            gameObject.transform.forward = playerMoving;

            animator.SetBool("isRunning", true);
        }

        // Changes the height position of the player..
        if (jumping && isOnGround)
        {
            playerVel.y += Mathf.Sqrt(jumpSpeed * -3.0f * gravityValue);
        }

        playerVel.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVel * Time.deltaTime);

        animator.SetBool("isPunching", false);

        if (punching)
        {
            animator.SetBool("isPunching", true);
        }
    }

    public void onMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void onJump(InputAction.CallbackContext context)
    {
        jumping = context.action.triggered;
    }
    
    public void onPunch(InputAction.CallbackContext context)
    {
        punching = context.action.triggered;
    }
}
