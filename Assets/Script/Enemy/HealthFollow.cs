using UnityEngine;

public class HealthFollow : MonoBehaviour
{
    public Transform enemy;
    public Vector3 offset;

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