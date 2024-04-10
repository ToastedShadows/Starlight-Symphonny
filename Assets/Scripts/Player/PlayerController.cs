using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask encounterLayer;

    public event Action OnEncountered;

    private Vector2 input;
    private Vector2 lastInput;
    private Rigidbody2D body;
    private bool isMoving;
    private bool canMove = true; // Flag to indicate whether the player can move

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (canMove) // Only handle movement if the player can move
            HandleMovement();
    }

    private void Update()
    {
        if (canMove) // Only handle input if the player can move
            HandleUpdate();
    }

    public void HandleUpdate()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("moveX", input.x);
        animator.SetFloat("moveY", input.y);

        if (input != Vector2.zero)
        {
            lastInput = input;
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
        }
        else
        {
            isMoving = false;
            animator.SetBool("isMoving", isMoving);

            if (lastInput != Vector2.zero && lastInput.y != -1)
            {
                animator.SetFloat("moveX", lastInput.x);
                animator.SetFloat("moveY", lastInput.y);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
            Interact();

        CheckForEncounters();
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
        Vector3 interactPos = transform.position + new Vector3(lastInput.x, lastInput.y, 0f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(interactPos, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Interactable"))
            {
                collider.GetComponent<Interactable>()?.Interact();
            }
        }
    }

    private void CheckForEncounters()
    {
        Collider2D encounterCollider = Physics2D.OverlapCircle(transform.position, 0.2f, encounterLayer);
    
        if (encounterCollider != null)
        {
            // Check if the player is already in an encounter
            if (canMove)
            {
                canMove = false; // Prevent further movement
                OnEncountered?.Invoke(); // Trigger the encounter
            }
        }
    }
}
