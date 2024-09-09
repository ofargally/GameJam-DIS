using UnityEngine;
using UnityEngine.SceneManagement;
public class resume : MonoBehaviour
{
    public void ResumeGame()
    {
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1;
    }
}
