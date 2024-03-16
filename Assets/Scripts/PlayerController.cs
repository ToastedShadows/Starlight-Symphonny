using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;

    private Vector2 input;

    private Animator animator;

    public LayerMask solidObjectsLayer; // Changed private to public

    public LayerMask interactableLayer;

    private void Start() // Changed from Update to Start
    {
        animator = GetComponent<Animator>(); // Added this line to get the Animator component
    }

    private void Update()
    {
        if (!isMoving) //checks if equal to false 
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0; // Added missing semicolon

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x); // Added missing semicolon and corrected syntax
                animator.SetFloat("moveY", input.y); // Corrected syntax

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
        
                if (isWalkable(targetPos))
                    StartCoroutine(Move(targetPos)); // Added parentheses to StartCoroutine
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
            Interact(); // Added missing parentheses
    }

    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        Debug.DrawLine(transform.position, interactPos, Color.red, 1f); // Added missing semicolon and corrected syntax
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true; // Added semicolon here
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)//if anything bigger than 0 move character
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos; // Added semicolon here

        isMoving = false;
    }

    private bool isWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactableLayer) != null)
        {
            return false; // Added semicolon here
        }
        
        return true;
    }
}