using UnityEngine;

public class Player_1 : PlayerController
{
    public static Player_1 Instance;

    protected override void Awake() {
        base.Awake();
        Instance = this;
        Aim();
    }

    override protected void Aim()
    {
        Vector2 spawnPosition = transform.position;
        spawnPosition.x += 1.5f;
        GameObject ball = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        Projectile projectile = ball.GetComponent<Projectile>();
        projectile.setMinAngle(10);
        projectile.setMaxAngle(90);

        //change states
        Projectile.EOnProjectileFires += () => stateController.TransitionToState(StateController.PlayerState.Fire);

        base.Aim();
    }
}