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
        SceneManager.LoadScene("Level_2");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        //Debug.Log("Load Menu");
        SceneManager.LoadScene("Menu");
    }

    public void Next()
    {
        Debug.Log("Load next level");
    }
}
