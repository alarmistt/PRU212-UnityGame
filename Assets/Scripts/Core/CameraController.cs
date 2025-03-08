using UnityEngine;
public class CameraController : MonoBehaviour
{
    //[SerializeField]
    //private Transform player;

    //[Range(0, 10)]
    //public float smoothFactor;

    //private Vector3 offset;
    //public Vector3 minValue, maxValue;
    //Vector3 playerPosition;

    //// Start is called before the first frame update 
    //void Start()
    //{
    //    offset = new Vector3(0, 0, -10);
    //}

    // Update is called once per frame 
    //    void FixedUpdate()
    //    {
    //        playerPosition = player.position + offset;
    //        transform.position = playerPosition;

    //        Vector3 boundPos = new Vector3(
    //            Mathf.Clamp(playerPosition.x, minValue.x, maxValue.x),
    //            Mathf.Clamp(playerPosition.y, minValue.y, maxValue.y),
    //            Mathf.Clamp(playerPosition.z, minValue.z, maxValue.z));

    //        transform.position = Vector3.Lerp(transform.position, boundPos,
    //smoothFactor);
    //    }
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    // follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;


    private void Update()
    {
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);
        
        // follow player
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}