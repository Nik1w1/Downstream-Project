using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    [Header("Audio Stuff")]
    public GameObject MenuTheme; //attach menu theme here
    AudioSource AS_MenuTheme;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        AS_MenuTheme = MenuTheme.GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            pauseMenuUI.SetActive(false);
            //AS_MenuTheme.volume = 1f;
            StartCoroutine(FadeIn(AS_MenuTheme, 2f)); //fading for 2 seconds
        }
        else //In-game scene
        {
            pauseMenuUI.SetActive(false);
            GameIsPaused = false;
            AS_MenuTheme.volume = 0f;
        }



    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        StartCoroutine(FadeOut(AS_MenuTheme, 1f)); //fading out for 1 seconds
        Time.timeScale = 1f; //Resumes time at normal speed
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        AS_MenuTheme.volume = 1f; 
        Time.timeScale = 0f; //Speed at which time passes in the game
        GameIsPaused = true;
    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game is quitting");
    }
    
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float startVolume = 0.2f;

        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = 1f;
    }
}
