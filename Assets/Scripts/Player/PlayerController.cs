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
    private bool canMove = true;
    private bool isTransitioning = false; // Add this flag

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (canMove && !isTransitioning && !DialogManager.Instance.IsDialogActive())
            HandleMovement();
    }

    private void Update()
    {
        if (canMove && !isTransitioning && !DialogManager.Instance.IsDialogActive())
            HandleUpdate();
    }

    public void HandleUpdate()
    {
        if (isTransitioning) return; // Skip updates if transitioning

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (animator != null)
        {
            if (input != Vector2.zero)
            {
                lastInput = input;
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
            }
            else
            {
                animator.SetFloat("moveX", lastInput.x);
                animator.SetFloat("moveY", lastInput.y);
            }

            animator.SetBool("isMoving", input != Vector2.zero);
        }

        if (Input.GetKeyDown(KeyCode.Z))
            Interact();

        CheckForEncounters();
    }

    private void HandleMovement()
    {
        if (input != Vector2.zero)
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
            if (canMove)
            {
                canMove = false;
                OnEncountered?.Invoke();
            }
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        string tag = other.collider.tag;

        if (tag.Equals("Coin"))
        {
            Debug.Log("hit coin");
            Destroy(other.gameObject);
        }
    }

    public void StartSceneTransition()
    {
        isTransitioning = true; // Set transitioning flag to true
    }
}
