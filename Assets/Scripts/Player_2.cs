using UnityEngine;

public class Player_2 : PlayerController
{
    public static Player_2 Instance;

    protected override void Awake() {
        base.Awake();
        Instance = this;
        Idle();
    }

    override protected void Aim()
    {
        Vector2 spawnPosition = transform.position;
        spawnPosition.x -= 1.5f;
        GameObject ball = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        Projectile projectile = ball.GetComponent<Projectile>();
        projectile.setMinAngle(90);
        projectile.setMaxAngle(180);

        //change states
        Projectile.EOnProjectileFires += () => stateController.TransitionToState(StateController.PlayerState.Fire);

        base.Aim();
    }

}
