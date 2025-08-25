using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int StepsTaken;
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
        StepsTaken = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            TakeStep(); // Call the method to take a step when the Up Arrow key is pressed
        }
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

}
    
