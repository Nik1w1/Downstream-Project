using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping; //smoothing factor
    public Transform player; //target to follow
    private Vector3 vel = Vector3.zero; //velocity reference for SmoothDamp
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        targetPosition.z = transform.position.z; //keep original z position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, damping * Time.fixedDeltaTime);
        //transform.position = smoothedPosition;
    }
}
