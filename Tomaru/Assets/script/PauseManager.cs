using UnityEngine;
using UnityEngine.InputSystem;

///////////////////Pause////////////////////
public class PauseManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    ///////////////////InputAction////////////////////
    public InputActionReference pauseAction;

    ///////////////////PauseRefernce////////////////////
    void OnEnable()
    {
        pauseAction.action.performed += OnPause;
    }

    void OnPause(InputAction.CallbackContext context)
    {
        Debug.Log("ESC pressed");
    }
}
