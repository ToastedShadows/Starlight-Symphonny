using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForeverOff : MonoBehaviour
{
    public GameObject door;
    public int requiredButtons = 2; // Number of buttons required to open the door
    public int buttonsPressed = 0; // Counter for the number of buttons pressed
    private bool doorOpened = false; // Flag to track if the door is opened

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!doorOpened && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Button"))) // Check if the door is not opened yet and collided object is either a player or a button
        {
            buttonsPressed++;
            Debug.Log("Button Pressed! Total Buttons Pressed: " + buttonsPressed);
            if (buttonsPressed >= requiredButtons)
            {
                door.SetActive(false);
                doorOpened = true; // Set the doorOpened flag to true
                Debug.Log("Door Opened!");
            }
        }
    }
}