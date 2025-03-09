using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum PlatformType { Vertical, Horizontal }

    [Header("Platform Settings")]
    [SerializeField] private PlatformType platformType;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private Vector3 targetPosition;

    private void Start()
    {
        transform.position = pointA.position;
        targetPosition = pointB.position;
    }

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
        }
    }
}
