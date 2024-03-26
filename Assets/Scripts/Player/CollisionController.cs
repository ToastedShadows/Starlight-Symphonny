using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public LayerMask collisionLayers; // Define which layers to collide with

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collisionLayers == (collisionLayers | (1 << collision.gameObject.layer)))
        {
            Debug.Log("Enter");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collisionLayers == (collisionLayers | (1 << collision.gameObject.layer)))
        {
            Debug.Log("Stay");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collisionLayers == (collisionLayers | (1 << collision.gameObject.layer)))
        {
            Debug.Log("Exit");
        }
    }
}