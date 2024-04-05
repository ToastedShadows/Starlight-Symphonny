using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForeverOff : MonoBehaviour
{
    public GameObject door;
    public int requiredButtons = 2; // Number of buttons required to open the door
    private static HashSet<GameObject> pressedButtons = new HashSet<GameObject>(); // HashSet to store pressed buttons
    private static bool doorOpened = false; // Flag to track if the door is opened

    // Function to handle button press
    public void PressButton(GameObject button)
    {
        if (!doorOpened && !pressedButtons.Contains(button)) // Check if the door is not opened yet and the button is not already pressed
        {
            pressedButtons.Add(button); // Add the button to the HashSet of pressed buttons
            Debug.Log("Button Pressed! Total Buttons Pressed: " + pressedButtons.Count);
            if (pressedButtons.Count >= requiredButtons)
            {
                door.SetActive(false);
                doorOpened = true; // Set the doorOpened flag to true
                Debug.Log("Door Opened!");
            }
        }
        else
        {
            Debug.Log("Button already pressed or door already opened!"); // Inform that the button has already been pressed or the door is already opened
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the player interacts with the button
        {
            PressButton(gameObject); // Call the PressButton method with the current button game object
        }
    }
}
