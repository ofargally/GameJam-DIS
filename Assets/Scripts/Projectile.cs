using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public float power = 5f;  // Initial magnitude of the velocity (speed)
    public float angle = 45f; // Angle in degrees
    public GameObject sliderPrefab; // Reference to the slider prefab

    private Rigidbody2D rb;
    private LineRenderer lr;
    private Slider sliderInstance; // Instance of the slider
    private bool hasLaunched = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();

        // Instantiate the Slider prefab when the game starts
        if (sliderPrefab != null)
        {
            GameObject sliderObj = Instantiate(sliderPrefab, FindFirstObjectByType<Canvas>().transform); // Instantiate in the UI Canvas
            sliderInstance = sliderObj.GetComponent<Slider>();
        }
        else
        {
            Debug.LogError("Slider prefab is not assigned!");
        }
    }

    void Update()
    {
        // On spacebar press, get the current slider value and launch the projectile
        if (Input.GetKeyDown(KeyCode.Space) && !hasLaunched)
        {
            if (sliderInstance != null)
            {
                power = sliderInstance.value;
                Debug.Log("Slider Value (Power): " + power);
            }

            // Launch the projectile
            LaunchProjectile();
            hasLaunched = true;

            // Optionally, destroy the slider UI after launching the projectile
            Destroy(sliderInstance.gameObject);
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

        // Optional: Show the trajectory using LineRenderer
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
