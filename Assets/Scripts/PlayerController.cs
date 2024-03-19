using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private Vector2 input;
    private Vector2 lastInput; // Store the last non-zero input direction
    private Rigidbody2D body;
    private bool isMoving;

    private Animator animator;

    public LayerMask interactableLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveCharacter(); // Handle movement in FixedUpdate for physics consistency
    }

    private void MoveCharacter()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // Set animator parameters for movement
        animator.SetFloat("moveX", input.x);
        animator.SetFloat("moveY", input.y);

        if (input != Vector2.zero)
        {
            lastInput = input; // Store the last non-zero input direction
            isMoving = true;
            animator.SetBool("isMoving", isMoving);

            Vector2 movement = input.normalized * moveSpeed * Time.fixedDeltaTime;
            Vector2 newPosition = body.position + movement;

            body.MovePosition(newPosition);
        }
        else
        {
            isMoving = false;
            animator.SetBool("isMoving", isMoving);

            // Face the last inputted direction when not moving and last input direction is not down
            if (lastInput != Vector2.zero && lastInput.y != -1)
            {
                // Set animator parameters for facing direction
                animator.SetFloat("moveX", lastInput.x);
                animator.SetFloat("moveY", lastInput.y);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            Interact();
    }

    private void Interact()
    {
        // Calculate the facing direction manually
        Vector3 facingDir = Vector3.right * input.x + Vector3.up * input.y;

        // Normalize the facing direction if it's not zero
        if (facingDir != Vector3.zero)
            facingDir.Normalize();

        // Calculate the interaction position based on the facing direction
        Vector3 interactPos = transform.position + facingDir;

        Debug.DrawLine(transform.position, interactPos, Color.red, 1f);
    }
}