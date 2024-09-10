using UnityEngine;

public class Player_2 : PlayerController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    override protected void Idle()
    {
        if (!(TurnManager.isPlayer1Turn))
        {
            base.Idle();
        }
    }

    override protected void Aim()
    {
        //TODO: flip the projectile
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        Projectile projectile = ball.GetComponent<Projectile>();
        projectile.setMinAngle(90);
        projectile.setMaxAngle(180);

        //change states
        Projectile.EOnProjectileFires += () => stateController.TransitionToState(StateController.PlayerState.Fire);

        base.Aim();
    }

}
