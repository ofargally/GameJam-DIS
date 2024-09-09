using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    // basic projectile collision script set up to
    // delete after a certain amount of time
    private int timer;

    [Header("Despawn Settings")]
    public int despawnTime;

    void Start() {
        timer = despawnTime;
    }

    void Update() {
        timer -= 1;
        if (timer <= 0) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.name == "player") {
            // deal damage to the player
        }
    }
}
