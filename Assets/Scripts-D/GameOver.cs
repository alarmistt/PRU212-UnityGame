using UnityEngine;
using UnityEngine.SceneManagement;

public class DGameOver : MonoBehaviour
{
    
    public void Menu()
    {
        //Debug.Log("Reload Menu ");
        SceneManager.LoadScene("Menu");
    }

    public void Retry()
    {
        PlayerPrefs.SetInt("CurentCoin", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
