using UnityEngine;
using UnityEngine.SceneManagement;

public class quitToMenu : MonoBehaviour
{
    public void quitGame()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1;
    }
}
