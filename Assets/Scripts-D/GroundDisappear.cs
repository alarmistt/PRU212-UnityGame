using UnityEngine;

public class DGroundDisappear : MonoBehaviour
{
    private bool isPlayerOnGround = false;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOnGround)
        {
            timer += Time.deltaTime;
            if (timer >= 0.15f)
            {
                
                Destroy(gameObject); 
            }
        }
        else
        {
            
            timer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnGround = false;
        }
    }
}
