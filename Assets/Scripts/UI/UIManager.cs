using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject PauseAll;
    public GameObject PauseGeneral;
    public GameObject PauseMisc;
    public GameObject PauseAudio;
    public GameObject StartMenu;
    public GameObject head;

    private bool paused;
    public bool returning;
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        //head = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && paused == false)
        {
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Start") && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Credits"))
            {
                pause();
            }
        }
        else
        if (Input.GetButtonDown("Pause") && paused == true)
        {
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Start") && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Credits"))
            {
                unpause();
            }
        }
    }

    public void SetQuality(int qualityIndex)
    {

        QualitySettings.SetQualityLevel(qualityIndex);

    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void RestartOnClick()
    {
        SceneManager.LoadScene("Start");
        Time.timeScale = 1f;
        SoundManager.PlaySound("spit");
    }

    public void ExitOnClick()
    {
        Application.Quit();
        Debug.Log("Exited");
        SoundManager.PlaySound("spit");
    }

    public void StartOnClick()
    {
        SceneManager.LoadScene("Main Scene");
        SoundManager.PlaySound("spit");
    }

    public void MenuOnClick()
    {
        PauseGeneral.SetActive(true);
        PauseAudio.SetActive(false);
        head.SetActive(false);
        if (returning == false)
        {
            SoundManager.PlaySound("spit");
        }
        if (returning == true)
        {
            SoundManager.PlaySound("spit");
        }
    }

    public void UnpauseOnClick()
    {
        unpause();
        SoundManager.PlaySound("spit");
    }

    public void CreditsOnClick()
    {
        SceneManager.LoadScene("Credits");
        SoundManager.PlaySound("spit");
    }

    public void RespawnOnClick()
    {
        unpause();
        HealthRespawnBehaviour.startRespawn();
        SoundManager.PlaySound("spit");
    }

    private void pause()
    {
        Scene currentscene = SceneManager.GetActiveScene();

        PauseAll.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (currentscene.name != "Start")
        {
            Time.timeScale = 0f;

            head.GetComponent<HeadBehaviour>().input_enabled = false;
            head.GetComponent<MovementBehaviour2>().input_enabled = false;
        }

        
        paused = true;
    }

    private void unpause()
    {
        Scene currentscene = SceneManager.GetActiveScene();

        Time.timeScale = 1f;
        paused = false;

        PauseAll.SetActive(false);

        if (currentscene.name != "Start")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            head.GetComponent<HeadBehaviour>().input_enabled = true;
            head.GetComponent<MovementBehaviour2>().input_enabled = true;                  //might create problems if you can pause unpause out of input disability
        }

        if (currentscene.name == "Start")
        {
            StartMenu.SetActive(true);
            head.SetActive(true);
        }
    }
}