using UnityEngine;
using UnityEngine.UI;

public class Bind : MonoBehaviour
{
    [Header("Accessing Bound State from PlayerController")]
    public GameObject Player; //Attatch the Player GameObject in the Inspector
    private PlayerController playerControllerScript; // Reference to the PlayerController script
    private bool isBound = false; // Variable to track bound state, should be accessed from PlayerController
    
    Animator _animator;
    string _currentState;
    const string BIND = "BindStart";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        playerControllerScript = Player.GetComponent<PlayerController>();
        isBound = playerControllerScript.isBound; // Initialize isBound from PlayerController
    }

    // Update is called once per frame
    void Update()
    {
        isBound = playerControllerScript.isBound; // Update isBound from PlayerController
        _animator.SetBool("isBound", isBound);

    }

    public void OnBindActivate()
    {
        Debug.Log("Bind Activated");
        if (!IsAnimationPlaying(_animator, BIND))
        {
            ChangeAnimationState(BIND);
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
