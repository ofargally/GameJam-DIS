using System.Collections;
using Unity.VisualScripting;
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
    private string _currentState = "";
    protected Coroutine anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
        stateController = new StateController(StateController.PlayerState.Idle);
        animationController = new AnimationController(idleAnimation, aimAnimation, fireAnimation, hitAnimation);
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void StartTurn(){
        stateController.TransitionToState(StateController.PlayerState.Aim);
    }

    public void EndTurn(){
        stateController.TransitionToState(StateController.PlayerState.Idle);
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
                if(_currentState == "Idle") return;
                _currentState ="Idle";
                Idle();
                break;
            case StateController.PlayerState.Aim:
                if(_currentState == "Aim") return;
                Debug.Log("Entering Aim for " + gameObject.name);
                _currentState ="Aim";
                Aim();
                break;
            case StateController.PlayerState.Fire:
                if(_currentState == "Fire") return;
                _currentState ="Fire";
                Fire();
                break;
            case StateController.PlayerState.Hit:
                if(_currentState == "Hit") return;
                _currentState ="Hit";
                Hit();
                break;
            default:
                _currentState ="";
                Debug.Log("not working");
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
        stateController.TransitionToState(StateController.PlayerState.Idle);
            
        if(anim != null) StopCoroutine(anim);
        anim = StartCoroutine(playAimAnimation());
    }

    protected virtual void Aim() {
        if(anim != null) StopCoroutine(anim);
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
        if(other.gameObject.CompareTag("Projectile"))
            stateController.TransitionToState(StateController.PlayerState.Hit);
    }
}
