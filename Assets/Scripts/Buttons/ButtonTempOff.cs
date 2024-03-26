using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTempOff : MonoBehaviour
{
    public GameObject door;

    private void OnCollisionEnter2D(Collision2D other) // "OnCollisionEnter2D" instead of "OncollisionEnter2D"
    {
        if (other.gameObject.CompareTag("Player")) // "other.gameObject" instead of "other.GameObject"
        {
            door.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D other) // "OnCollisionExit2D" instead of "OncollisionExit2D"
    {
        if (other.gameObject.CompareTag("Player")) // "other.gameObject" instead of "other.GameObject"
        {
            door.SetActive(true);
        }
    }
}