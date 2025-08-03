using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public InputAction pauseAction;

    public GameObject pauseCanvas;
    public bool isPaused;

    public Slider sensSlider;
    public Slider volumeSlider;

    public Button returnButton;
    public Button quitButton;

    public AudioMixer masterChannel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Unpause();
        pauseAction = InputSystem.actions.FindAction("Pause");
        sensSlider.value = PlayerPrefs.GetFloat("Mouse Sens");;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("Mouse Sens", sensSlider.value);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        masterChannel.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
        returnButton.onClick.AddListener(Unpause);
        quitButton.onClick.AddListener(Exit);
        if (pauseAction.triggered && !isPaused)
        {
            Pause();
        }
        else if (pauseAction.triggered && isPaused)
        {
            Unpause();
        }
    }

    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        AudioListener.pause = true;
        pauseCanvas.SetActive(true);
    }
    void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        AudioListener.pause = false;
        pauseCanvas.SetActive(false);
    }

    void Exit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
