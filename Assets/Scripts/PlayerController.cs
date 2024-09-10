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

    protected StateController stateController;
    private AnimationController animationController;
    private SpriteRenderer playerSpriteRenderer;
    private float frameTimer;
    private int frameIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateController = new StateController(StateController.PlayerState.Idle);
        animationController = new AnimationController();
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (stateController.GetPlayerState())
        {
            case StateController.PlayerState.Idle:
                break;
            case StateController.PlayerState.Aim:
                break;
            case StateController.PlayerState.Fire:
                break;
            case StateController.PlayerState.Hit:
                break;
        }
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

    protected virtual void Idle()
    {

    }

    protected virtual void Aim() {
        GameObject proj = Instantiate(ballPrefab, transform.position, Quaternion.identity);

        //change states
        Projectile.EOnProjectileFires += () => stateController.TransitionToState(StateController.PlayerState.Fire);
    }

}
