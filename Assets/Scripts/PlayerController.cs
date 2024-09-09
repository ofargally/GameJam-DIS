using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject ballPrefab;
    public float throwForce = 1000;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Get the input from the player - use space to jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowBall();
        }
    }

    void ThrowBall()
    {
        //Throw the ball
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        ball.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
    }
}
