using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject ballPrefab;

    public int health = 10;
    public float throwForce = 1000;

    [Header("Animation Sprites")]
    public Sprite[] idleAnimation;
    public Sprite[] aimAnimation;
    public Sprite[] fireAnimation;
    public Sprite[] hurtAnimation;

    public float framesPerSecond;

    private StateController stateController;
    private AnimationController animationController;
    private SpriteRenderer playerSpriteRenderer;
    private float frameTimer;
    private int frameIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateController = new StateController(StateController.PlayerState.Idle);
        animationController = new AnimationController(idleAnimation, aimAnimation, fireAnimation, hurtAnimation);
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        switch(stateController.GetPlayerState()) {
            case StateController.PlayerState.Idle:
                break;
            case StateController.PlayerState.Aim:
                break;
            case StateController.PlayerState.Fire:
                break;
            case StateController.PlayerState.Hit:
                break;
        }
        //Get the input from the player - use space to jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowBall();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            TakeDamage(1);
        }   
    }

    void ThrowBall()
    {
        //Throw the ball
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().AddForce(transform.forward * 1000);
        ball.GetComponent<Rigidbody2D>().AddForce(transform.up * 1000);
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        //TODO: Implement death
    }

    private void Idle() {
        
    }
    private void Aim() {

    }
    
}
