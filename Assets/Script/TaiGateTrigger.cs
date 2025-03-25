using UnityEngine;
using System.Collections;

public class TaiGateTrigger : MonoBehaviour
{
    [Header("setting")]
    public GameObject gate;   
    public float closeSpeed = 10f; 
    public float openSpeed = 10f;  
    public Vector3 closedPosition; 
    public Vector3 openedPosition;
    public AudioClip audioClip;

    private bool isGateClosed = false;

    private void Start()
    {

        if (gate != null)
        {
            gate.transform.position = openedPosition;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isGateClosed)
        {
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            CloseGate();
        }
    }

    private void CloseGate()
    {
        isGateClosed = true;
        Debug.Log("close");
        
        StartCoroutine(MoveGate(closedPosition, closeSpeed));
    }

    public void OpenGate()
    {
        isGateClosed = false;
        Debug.Log("open");

        StartCoroutine(MoveGate(openedPosition, openSpeed));
    }

    private IEnumerator MoveGate(Vector3 targetPosition, float speed)
    {
        while (Vector3.Distance(gate.transform.position, targetPosition) > 0.01f)
        {
            gate.transform.position = Vector3.MoveTowards(gate.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("C?a ?ã ??t v? trí ?ích!");
    }

    public bool IsDoorClosed()
    {
        return isGateClosed;
    }
}
