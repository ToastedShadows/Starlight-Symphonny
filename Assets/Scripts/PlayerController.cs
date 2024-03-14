using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;

    private Vector2 input;

    private void Update()
    {
        if (!isMoving) //checks if equal to false 
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input != Vector2.zero)
            {
                var targetPos = transform.position + new Vector3(input.x, input.y, 0); // Updated to add input directly to position
                StartCoroutine(Move(targetPos)); // Added parentheses to StartCoroutine
            }
        }
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
}