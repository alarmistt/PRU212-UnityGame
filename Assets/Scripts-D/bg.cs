using UnityEngine;

public class DBgFollow : MonoBehaviour
{
    public Transform mainCam;
    public Transform midBg;
    //public float length;

    private Vector3 lastCamPos;

    void Awake()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main?.transform;
        }
        lastCamPos = mainCam.position;
    }

    void Update()
    {
        if (mainCam == null) return;

        float moveDistance = mainCam.position.x - lastCamPos.x;
        midBg.position += new Vector3(moveDistance, 0, 0);

        lastCamPos = mainCam.position;
    }
}

