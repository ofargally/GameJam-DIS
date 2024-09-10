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
    public Sprite[] hitAnimation;

    public float framesPerSecond;

    protected StateController stateController;
    private AnimationController animationController;
    protected SpriteRenderer playerSpriteRenderer;
    private float frameTimer;
    private int frameIndex;
    protected Coroutine anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        stateController = new StateController(StateController.PlayerState.Idle);
        animationController = new AnimationController();
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Start() {
        anim = StartCoroutine(playIdleAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        switch (stateController.GetPlayerState())
        {
            case StateController.PlayerState.Idle:
                Idle();
                break;
            case StateController.PlayerState.Aim:
                Aim();
                break;
            case StateController.PlayerState.Fire:
                Fire();
                break;
            case StateController.PlayerState.Hit:
                Hit();
                break;
        }
    }

    protected void TakeDamage(int damage)
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

    protected virtual void Idle() {
        stateController.TransitionToState(StateController.PlayerState.Aim);
            
        StopCoroutine(anim);
        anim = StartCoroutine(playAimAnimation());
    }

    protected virtual void Aim() {
        StopCoroutine(anim);
        anim = StartCoroutine(playFireAnimation());
    }

    protected virtual void Fire() {
        if(anim == null) {
            stateController.TransitionToState(StateController.PlayerState.Idle);

            anim = StartCoroutine(playIdleAnimation());
        }
    }

    protected virtual void Hit() {
        TakeDamage(4);
        
        StopCoroutine(anim);
        anim = StartCoroutine(playHitAnimation());

        if(anim == null) {
            stateController.TransitionToState(StateController.PlayerState.Idle);
        }
    }

    public IEnumerator playIdleAnimation() {
        int counter = 0;
        while(counter < idleAnimation.Length) {
            playerSpriteRenderer.sprite = idleAnimation[counter];
            counter++;
            counter %= idleAnimation.Length;
            yield return new WaitForSeconds(1 / framesPerSecond);
        }
    }
    public IEnumerator playAimAnimation() {
        int counter = 0;
        while(counter < aimAnimation.Length) {
            playerSpriteRenderer.sprite = aimAnimation[counter];
            counter++;
            yield return new WaitForSeconds(1 / framesPerSecond);
        }
    }
    public IEnumerator playFireAnimation() {
        int counter = 0;
        while(counter < fireAnimation.Length) {
            playerSpriteRenderer.sprite = fireAnimation[counter];
            counter++;
            yield return new WaitForSeconds(1 / framesPerSecond);
        }
    }
    public IEnumerator playHitAnimation() {
        int counter = 0;
        while(counter < hitAnimation.Length) {
            playerSpriteRenderer.sprite = hitAnimation[counter];
            counter++;
            yield return new WaitForSeconds(1 / framesPerSecond);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        stateController.TransitionToState(StateController.PlayerState.Hit);
    }
}
