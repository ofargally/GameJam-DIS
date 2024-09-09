using UnityEngine;
using UnityEngine.UI;

public class SliderMovement : MonoBehaviour
{
    public Slider slider;       // Reference to the UI Slider
    public float minValue = 0f; // Minimum slider value
    public float maxValue = 1f; // Maximum slider value
    public float moveSpeed = 0.5f; // Rate of moving between min and max
    public GameObject projectilePrefab; // What we'll instantiate and send on its way.

    private bool isMoving = true; // Determines whether the slider is still moving
    private float direction = 1f; // Direction in which the slider is moving (1 for right, -1 for left)

    void Start()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }

        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = minValue;
    }

    void Update()
    {
        if (isMoving)
        {
            // Move the slider between min and max value
            slider.value += direction * moveSpeed * Time.deltaTime;

            // If we hit max or min, change direction
            if (slider.value >= maxValue || slider.value <= minValue)
            {
                direction *= -1f;
            }
        }

        // Check if spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopAndReturnValue();
        }
    }

    float StopAndReturnValue()
    {
        isMoving = false;
        Debug.Log("Slider Value: " + slider.value);  // Return the value in some form (e.g., log it)

        // Optional: perform any other logic for handling the returned value.
        // Destroy the game object
        Destroy(gameObject);
	return slider.value;
    }

    void InstantiateProjectile()
    {
        // Instantiate the projectile at the spawn point
        // GameObject projectile = Instantiate(projectilePrefab, PLAYER'S POSITION, PLAYER'S ROTATION);

        // Assign the current slider value (speed) to the projectile's velocity
       // Rigidbody rb = projectile.GetComponent<Rigidbody>();
       // if (rb != null)
       // {
            // We also would need to multiply by some thing of vector3.up
          //  Vector3 velocity = PLAYER'S POSITION * slider.value;
         //   rb.velocity = velocity;
     	// }

        Debug.Log("Projectile Speed: " + slider.value);  // Log the projectile's speed
    }
}

