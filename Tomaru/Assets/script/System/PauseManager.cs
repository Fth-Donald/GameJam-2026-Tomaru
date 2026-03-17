using UnityEngine;
using UnityEngine.InputSystem;

///////////////////Pause////////////////////
public class PauseManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    ///////////////////InputAction////////////////////
    ///
    public InputActionReference pauseAction;

    public GameObject pauseMenu;

    private bool isPaused = false;

    ///////////////////PauseRefernce////////////////////
    void Start()
    {
        pauseMenu.SetActive(false);
    }
    void OnEnable()
    {
        pauseAction.action.Enable();
        pauseAction.action.performed += OnPause;
    }

    void OnDisable()
    {
        pauseAction.action.performed -= OnPause;
        pauseAction.action.Disable();
    }

     void OnPause(InputAction.CallbackContext context)
    {
        Debug.Log("esc");
        isPaused = !isPaused;

        if (isPaused)
        {
            
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Debug.Log("pause");
    }
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Debug.Log("resume");
    }

}

