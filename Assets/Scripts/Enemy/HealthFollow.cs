using UnityEngine;

public class HealthFollow : MonoBehaviour
{
    public Transform enemy;
    public Vector3 offset;

    private void Start()
    {
        Collider2D col = enemy.GetComponent<Collider2D>();
        if (col != null)
        {
            float characterHeight = col.bounds.size.y;
            offset = new Vector3(0, characterHeight + 0.2f, 0); 
        }
    }


    private void LateUpdate()
    {
        if (enemy != null)
        {
            transform.position = enemy.position + offset;

            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(enemy.localScale.x);
            transform.localScale = scale;
        }
    }
}