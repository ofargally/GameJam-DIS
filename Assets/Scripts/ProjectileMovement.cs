using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ProjectileMovement : MonoBehaviour
{  
    // projectile movement will take a speed,
    // angle, and mass and plan out a parabola
    // for the projectile to move

    public float power = 5f;

    private Rigidbody2D rb;

    private LineRenderer lr;

    private Vector2 DragStartPos;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            DragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) {
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = (DragEndPos - DragStartPos) * power;

            Vector2[] trajectory = Plot(rb, (Vector2)transform.position, _velocity, 500);
            lr.positionCount = trajectory.Length;

            Vector3[] positions = new Vector3[trajectory.Length];
            for (int i = 0; i < positions.Length; i++) {
                positions[i] = trajectory[i];
            } 

            lr.SetPositions(positions);
        }

        if (Input.GetMouseButtonUp(0)) {
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = (DragEndPos - DragStartPos) * power;

            rb.linearVelocity = _velocity;
        }
    }

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps) {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;

        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;

        float drag = 1f - timestep * rigidbody.linearDamping;

        Vector2 moveStep = velocity * timestep;

        for (int i = 0; i < steps; i++) {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }
}
