using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync(2);
            Time.timeScale = 0;
        }
    }
}
