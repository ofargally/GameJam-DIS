using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    // basic projectile collision script set up to
    // delete after a certain amount of time

    [Header("Despawn Settings")]
    public float despawnTime;

    void Start() {
        Destroy(gameObject, despawnTime);
    }

    void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.name == "player") {
            // deal damage to the player
        }
    }
}
