using UnityEngine;

public class DGroundMovement : MonoBehaviour
{
    public float speed = 3f;
    public float distance = 10f;

    private Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPos + Vector3.right * movement;
    }
}
