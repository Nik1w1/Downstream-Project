using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int StepsTaken;
    private bool FootOn = True; //Which foot the player should take next step with True = Right, False = Left
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StepsTaken = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.ArrowUp))
        {
            StepsTaken++;
            Debug.Log("Steps taken: " + StepsTaken);
            //Add code to move the player forward
            //Add code to run animations
            if (FootOn)
            {
                Debug.Log("Right foot forward");
                //wait for animation to finish before allowing next step
                FootOn = false; // Switch to left foot for next step
            }
            else
            {
                Debug.Log("Left foot forward");
                //wait for animation to finish before allowing next step
                FootOn = true; // Switch to right foot for next step
            }
        }
    }

}
