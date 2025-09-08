using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping; //smoothing factor
    private Transform tran_player; //target to follow
    private Vector3 vel = Vector3.zero; //velocity reference for SmoothDamp
                                        // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [Header("Accessing CameraFollow Bool from PlayerController")]
    public GameObject Player; //Attatch the Player GameObject in the Inspector
    private PlayerController playerControllerScript; // Reference to the PlayerController script
    private bool cameraFollow = true; // Variable to track bound state, should be accessed from PlayerController
    void Start()
    {
        playerControllerScript = Player.GetComponent<PlayerController>();
        tran_player = Player.GetComponent<Transform>();
        //cameraFollow = playerControllerScript.cameraFollow; // Initialize cameraFollow from PlayerController

    }

    // Update is called once per frame
    void Update()
    {
        //cameraFollow = playerControllerScript.cameraFollow; // Update cameraFollow from PlayerController

    }

    private void FixedUpdate()
    {
        if (cameraFollow)
        {
            Vector3 targetPosition = tran_player.position + offset;
            targetPosition.z = transform.position.z; //keep original z position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, damping * Time.fixedDeltaTime);
            //transform.position = smoothedPosition;
        }   
    }
}
