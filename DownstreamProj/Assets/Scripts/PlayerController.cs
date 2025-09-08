using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //General Movement 
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    public bool isBound = false;
    public bool cameraFollow = true; //Referenced by CameraController script to check if it should follow the player
    //Set to false if player hits the scene boundary. 


    //For intro Scene walking
    private int StepsTaken;
    private bool FootOn = false; //Which foot the player should take next step with True = Right, False = Left
                                 // Start is called once before the first execution of Update after the MonoBehaviour is created

    //Player Animtion states

    Animator _animator;
    string _currentState;
    const string LEFT_STEP = "Left Step";
    const string RIGHT_STEP = "Right Step";
    const string STARTUP = "Start Up";

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name == "IntroScene")
        {
            StepsTaken = 0;
        }

        else
        {
            Debug.Log("not intro scene, should be able to move");
            rb = gameObject.GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "IntroScene")
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                TakeStep(); // Call the method to take a step when the Up Arrow key is pressed
            }
        }

        else
        {
            rb.linearVelocity = moveInput * moveSpeed;
            isBound = _animator.GetBool("isBound");

            //FOR TESTING PURPOSES - REMOVE LATER//
            bool boundstate = _animator.GetBool("isBound");
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (boundstate)
                {
                    Debug.Log("B key pressed - player is unbound");
                    _animator.SetBool("isBound", false);
                    moveSpeed = 5f; // Reset movement speed when unbound
                    return;
                }
                else
                {
                    BindPlayer();
                    return;
                }
            }
            //Testing code ends here//


        }
    }

    public void BindPlayer()
    {
        Debug.Log("Player is now bound");
        isBound = true;
        _animator.SetBool("isBound", true);
        moveSpeed = 10f; // Increase movement speed when bound
    }

    public void Move(InputAction.CallbackContext context)
    {
        //Debug.Log("Move input received: " + context.ReadValue<Vector2>());
        _animator.SetBool("isWalking", true);
        if (context.canceled)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetFloat("LastInputX", moveInput.x);
            _animator.SetFloat("LastInputY", moveInput.y);
        }
        moveInput = context.ReadValue<Vector2>();
        _animator.SetFloat("InputX", moveInput.x);
        _animator.SetFloat("InputY", moveInput.y);
    }
    void TakeStep()
    {
        // This method can be used to encapsulate the step-taking logic
        // It can be called from Update or other methods as needed
        if (!IsAnimationPlaying(_animator, LEFT_STEP) && !IsAnimationPlaying(_animator, RIGHT_STEP) && !IsAnimationPlaying(_animator, STARTUP))
        {
            StepsTaken++;
            Debug.Log("Steps taken: " + StepsTaken);
            if (StepsTaken == 1)
            {
                ChangeAnimationState(STARTUP);
                return; // Skip the rest of the method for the first step
            }
            else
            {
                if (FootOn)
                {
                    Debug.Log("Right foot forward");
                    ChangeAnimationState(RIGHT_STEP);
                    FootOn = false; // Switch to left foot for next step
                }
                else
                {
                    Debug.Log("Left foot forward");
                    ChangeAnimationState(LEFT_STEP);
                    FootOn = true; // Switch to right foot for next step
                }
            }

        }
    }

    private void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (newState == _currentState)
        {
            return;
        }

        //play the animation
        _animator.Play(newState);

        //reassign the current state
        _currentState = newState;

    }

    bool IsAnimationPlaying(Animator animator, string StateName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(StateName) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /** I realized I dont need this method... the camera stops annyways

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            cameraFollow = false;
            Debug.Log("Player hit boundary, camera follow disabled");
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            cameraFollow = true;
            Debug.Log("Player exited boundary, camera follow enabled");
        }
    }
    **/

}
    
