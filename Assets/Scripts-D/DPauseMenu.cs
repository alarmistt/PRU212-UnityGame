using UnityEngine;
using UnityEngine.SceneManagement;

public class DPauseMenu : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
