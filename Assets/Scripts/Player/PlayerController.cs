using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private Vector2 input;
    private Vector2 lastInput; // Store the last non-zero input direction
    private Rigidbody2D body;
    private bool isMoving;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement(); // Handle movement in FixedUpdate for physics consistency
    }

    public void Update()
    {
        HandleUpdate(); // Call HandleUpdate every frame from Update
    }

    public void HandleUpdate()
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

        if (Input.GetKeyDown(KeyCode.Z))
            Interact();
    }

    private void HandleMovement()
    {
        if (isMoving)
        {
            Vector2 movement = input.normalized * moveSpeed * Time.fixedDeltaTime;
            Vector2 newPosition = body.position + movement;
            body.MovePosition(newPosition);
        }
    }

    private void Interact()
    {
        // Calculate the interaction position based on the player's facing direction
        Vector3 interactPos = transform.position + new Vector3(lastInput.x, lastInput.y, 0f);

        // Check for collider with "Interactable" tag
        Collider2D[] colliders = Physics2D.OverlapCircleAll(interactPos, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Interactable"))
            {
                collider.GetComponent<Interactable>()?.Interact();
            }
        }
    }
}