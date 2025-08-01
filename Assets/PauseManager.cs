using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private InputAction pauseAction;

    public GameObject pauseCanvas;
    public bool isPaused;

    public Slider sensSlider;

    public Button returnButton;
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
        returnButton.onClick.AddListener(Unpause);
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
        pauseCanvas.SetActive(true);
    }
    void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        pauseCanvas.SetActive(false);
    }
}
