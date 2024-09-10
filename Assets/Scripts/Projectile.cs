using UnityEngine;
using UnityEngine.UI;
using System;  // Required for Action
using Unity.Collections;

public class Projectile : MonoBehaviour
{
    public delegate void OnProjectileHits();
    public static event OnProjectileHits EOnProjectileHits;
    public delegate void OnProjectileFire();
    public static event OnProjectileHits EOnProjectileFires;

    public float power = 5f;  // Initial magnitude of the velocity (speed)
    public float angle = 45f; // Angle in degrees

    public float decayTime; // Time before projectile is deleted
    public float sliderSpeed = 10f; // Speed at which sliders move back and forth
    public float minPower = 1f;    // Minimum power value
    public float maxPower = 10f;   // Maximum power value
    public float minAngle = 10f;   // Minimum angle value
    public float maxAngle = 80f;   // Maximum angle value

    private Rigidbody2D rb;
    private LineRenderer lr;
    private bool angleLocked = false; // Lock for angle
    private bool powerLocked = false; // Lock for power
    private bool hasLaunched = false;

    private float angleSliderValue;  // Current value for the angle slider
    private float powerSliderValue;  // Current value for the power slider

    private int angleDirection = 1;  // Direction for the angle slider movement
    private int powerDirection = 1;  // Direction for the power slider movement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();

        // Initialize slider values to start at their midpoints
        angleSliderValue = (minAngle + maxAngle) / 2f;
        powerSliderValue = (minPower + maxPower) / 2f;

        Destroy(gameObject, decayTime); // Destroy the object after a certain time
    }

    void Update()
    {
        if (!hasLaunched)
        {
            // Move angle value if it's not locked
            if (!angleLocked)
            {
                MoveAngleValue();
            }

            // Move power value if the angle is locked and power is not locked
            if (angleLocked && !powerLocked)
            {
                MovePowerValue();
            }

            // Lock in the angle when the player presses space the first time
            if (Input.GetKeyDown(KeyCode.Space) && !angleLocked)
            {
                angle = angleSliderValue;
                Debug.Log("Angle locked at: " + angle);
                angleLocked = true;
            }
            // Lock in the power when the player presses space the second time
            else if (Input.GetKeyDown(KeyCode.Space) && angleLocked && !powerLocked)
            {
                power = powerSliderValue;
                Debug.Log("Power locked at: " + power);
                powerLocked = true;
            }
            // Launch the projectile when the player presses space the third time
            else if (Input.GetKeyDown(KeyCode.Space) && angleLocked && powerLocked && !hasLaunched)
            {
                LaunchProjectile();
                hasLaunched = true;
            }

            // Show trajectory if not launched yet
            if (!hasLaunched)
            {
                DisplayTrajectory();
            }
        }
    }

    void MoveAngleValue()
    {
        // Oscillate the angle value between min and max
        angleSliderValue += sliderSpeed * angleDirection * Time.deltaTime;

        if (angleSliderValue >= maxAngle || angleSliderValue <= minAngle)
        {
            angleDirection *= -1; // Reverse direction when reaching the boundaries
        }

        Debug.Log("Current Angle: " + angleSliderValue); // Debugging to show the value changing
    }

    void MovePowerValue()
    {
        // Oscillate the power value between min and max
        powerSliderValue += sliderSpeed * powerDirection * Time.deltaTime;

        if (powerSliderValue >= maxPower || powerSliderValue <= minPower)
        {
            powerDirection *= -1; // Reverse direction when reaching the boundaries
        }

        Debug.Log("Current Power: " + powerSliderValue); // Debugging to show the value changing
    }

    void LaunchProjectile()
    {
        // Convert angle from degrees to radians
        float angleInRadians = angle * Mathf.Deg2Rad;

        // Calculate velocity components based on angle and power
        float velocityX = power * Mathf.Cos(angleInRadians);
        float velocityY = power * Mathf.Sin(angleInRadians);

        // Set the Rigidbody2D velocity using the calculated vector
        Vector2 _velocity = new Vector2(velocityX, velocityY);
        rb.linearVelocity = _velocity;

        // Clear the trajectory once launched
        lr.positionCount = 0;

        EOnProjectileFires?.Invoke();
    }

    void DisplayTrajectory()
    {
        float currentAngle = angle;
        float currentPower = power;
        // Use slider values if the sliders are available and not yet locked
        if (!angleLocked)
        {
            currentAngle = angleSliderValue;
        }
        if (!powerLocked && angleLocked)
        {
            currentPower = powerSliderValue;
        }
        // Convert angle from degrees to radians
        float angleInRadians = currentAngle * Mathf.Deg2Rad;

        // Calculate velocity components based on angle and power
        float velocityX = currentPower * Mathf.Cos(angleInRadians);
        float velocityY = currentPower * Mathf.Sin(angleInRadians);

        // Plot the trajectory
        Vector2 _velocity = new Vector2(velocityX, velocityY);
        Vector2[] trajectory = Plot(rb, (Vector2)transform.position, _velocity, 500);

        lr.positionCount = trajectory.Length;

        Vector3[] positions = new Vector3[trajectory.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = trajectory[i];
        }

        lr.SetPositions(positions);
    }

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];
        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;
        float drag = 1f - timestep * rigidbody.linearDamping;
        Vector2 moveStep = velocity * timestep;

        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }

    void OnDestroy()
    {
        EOnProjectileHits?.Invoke();
    }
}
