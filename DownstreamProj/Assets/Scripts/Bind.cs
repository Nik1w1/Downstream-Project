using UnityEngine;
using UnityEngine.UI;

public class Bind : MonoBehaviour
{

    Animator _animator;
    string _currentState;
    const string BIND = "BindStart";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

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
