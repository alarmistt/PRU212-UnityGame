using UnityEngine;
using UnityEngine.SceneManagement;

public class DDoorTrigger : MonoBehaviour
{

    public string nameScene;
    public void LoadScene()
    {
        SceneManager.LoadScene(nameScene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadScene();
        }
    }
}
