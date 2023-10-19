using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    // A variable to store the speed of the ship
    public float speed = 5f;

    // Update method to get the input from the user
    private void Update()
    {
        // Get the horizontal and vertical input from the user
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Create a vector2 to store the direction of the movement
        Vector2 direction = new Vector2(horizontal, vertical);

        // Normalize the direction vector to have a magnitude of 1
        direction.Normalize();

        // Call the Move method and pass the direction vector as a parameter
        Move(direction);
    }

    // Move method to move the ship according to the direction vector
    private void Move(Vector2 direction)
    {
        // Multiply the direction vector by the speed and delta time to get the velocity vector
        Vector2 velocity = direction * speed * Time.deltaTime;

        // Add the velocity vector to the current position of the ship
        transform.position += (Vector3)velocity;

        // Get the screen position of the ship
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        // If the ship goes off the screen, move it to the other side of the screen
        if (screenPosition.x > Screen.width)
        {
            screenPosition.x = 0;
        }
        else if (screenPosition.x < 0)
        {
            screenPosition.x = Screen.width;
        }
    }
}
