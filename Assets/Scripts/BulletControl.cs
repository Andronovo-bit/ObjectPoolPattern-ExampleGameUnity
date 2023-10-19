using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    private void Update()
    {
        // Get the direction up
        Vector3 direction = Vector3.up;

        // Normalize the direction vector to have a magnitude of 1
        direction.Normalize();

        // Get the time since the last frame
        float timeSinceLastFrame = Time.deltaTime;

        // Calculate the translation vector by multiplying the direction, speed, and time
        Vector3 translation = direction * speed * timeSinceLastFrame;

        // Move the bullet according to the translation vector
        transform.Translate(translation);
        
        //if bullet goes off the screen, call ReturnObject method and add it to pool
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height)
        {
            ObjectPoolManager.Instance.ReturnObject("Bullet", gameObject);
        }
    }
}
