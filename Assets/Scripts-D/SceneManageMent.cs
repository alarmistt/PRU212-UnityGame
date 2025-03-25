using UnityEngine;
using UnityEngine.SceneManagement;

public class DSceneManageMent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        PlayerPrefs.SetInt("CurentCoin", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level_2");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        PlayerPrefs.SetInt("CurentCoin", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Menu");
    }

    public void Next()
    {
        Debug.Log("Load next level");
    }
}
