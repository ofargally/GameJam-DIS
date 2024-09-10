using UnityEngine;

public class Player_1 : PlayerController
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
        if (TurnManager.isPlayer1Turn)
        {
            stateController.TransitionToState(StateController.PlayerState.Aim);
            
            StopCoroutine(animation);
            animation = StartCoroutine(playAimAnimation());
        }
    }

    override protected void Aim()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        //change states
        Projectile.EOnProjectileFires += () => stateController.TransitionToState(StateController.PlayerState.Fire);

        StopCoroutine(animation);
        animation = StartCoroutine(playFireAnimation());
    }
}