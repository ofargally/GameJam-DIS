using UnityEngine;

public class Player_2 : PlayerController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Aim();
    }

    // Update is called once per frame
    void Update()
    {

    }

    override protected void Idle()
    {
        if (!(TurnManager.isPlayer1Turn))
        {
            stateController.TransitionToState(StateController.PlayerState.Aim);
        }
    }

    override protected void Aim()
    {
        //TODO: flip the projectile
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        
        //change states
        Projectile.EOnProjectileFires += () => stateController.TransitionToState(StateController.PlayerState.Fire);
    }

}
