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
        PlayerPrefs.SetFloat("musicVolume", 0.5f);
        PlayerPrefs.Save();
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
        PlayerPrefs.SetInt("CurentCoin", 0);
        PlayerPrefs.Save();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
