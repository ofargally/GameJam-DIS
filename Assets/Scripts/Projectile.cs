using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public float power = 5f;  // Initial magnitude of the velocity (speed)
    public float angle = 45f; // Angle in degrees
    public GameObject powerSliderPrefab; // Reference to the power slider prefab
    public GameObject angleSliderPrefab; // Reference to the angle slider prefab

    public float decayTime; // time before projectile is deleted

    private Rigidbody2D rb;
    private LineRenderer lr;
    private Slider powerSliderInstance; // Instance of the power slider
    private Slider angleSliderInstance; // Instance of the angle slider
    private bool angleLocked = false; // Lock for angle
    private bool powerLocked = false; // Lock for power
    private bool hasLaunched = false;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();

        // Instantiate the angle and power slider prefabs when the game starts
        if (powerSliderPrefab != null && angleSliderPrefab != null)
        {
            GameObject powerSliderObj = Instantiate(powerSliderPrefab, FindFirstObjectByType<Canvas>().transform); // Instantiate in the UI Canvas
            powerSliderInstance = powerSliderObj.GetComponent<Slider>();

            GameObject angleSliderObj = Instantiate(angleSliderPrefab, FindFirstObjectByType<Canvas>().transform); // Instantiate in the UI Canvas
            angleSliderInstance = angleSliderObj.GetComponent<Slider>();
        }
        else
        {
            Debug.LogError("Power or Angle slider prefab is not assigned!");
        }

        Destroy(gameObject, decayTime); // destroy object after time to save space
    }

    void Update()
    {
        // First space press locks in the angle
        if (Input.GetKeyDown(KeyCode.Space) && !angleLocked)
        {
            if (angleSliderInstance != null)
            {
                angle = angleSliderInstance.value;
                Debug.Log("Slider Value (Angle): " + angle);
                angleLocked = true; // Lock the angle after the first press
            }
        }
        // Second space press locks in the power
        else if (Input.GetKeyDown(KeyCode.Space) && angleLocked && !powerLocked)
        {
            if (powerSliderInstance != null)
            {
                power = powerSliderInstance.value;
                Debug.Log("Slider Value (Power): " + power);
                powerLocked = true; // Lock the power after the second press
            }
        }
        // Final space press launches the projectile
        else if (Input.GetKeyDown(KeyCode.Space) && angleLocked && powerLocked && !hasLaunched)
        {
            // Launch the projectile
            LaunchProjectile();
            hasLaunched = true;

            // Optionally, destroy the slider UIs after launching the projectile
            Destroy(powerSliderInstance.gameObject);
            Destroy(angleSliderInstance.gameObject);
        }

        // Show trajectory if angle and power are set but not launched yet
        if (angleLocked && powerLocked && !hasLaunched)
        {
            DisplayTrajectory();
        }
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
    }

    void DisplayTrajectory()
    {
        // Convert angle from degrees to radians
        float angleInRadians = angle * Mathf.Deg2Rad;

        // Calculate velocity components based on angle and power
        float velocityX = power * Mathf.Cos(angleInRadians);
        float velocityY = power * Mathf.Sin(angleInRadians);

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
}
