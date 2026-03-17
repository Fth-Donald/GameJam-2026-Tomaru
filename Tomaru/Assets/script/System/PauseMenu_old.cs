using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu_old : MonoBehaviour
{
    public PauseManager pauseManager;

    //////////////////ContinueButton/////////////////////////
    public void ContinueButton()
    {
        pauseManager.ResumeGame();
    }
    ///////////////////SelectButton//////////////////
    public void GoToSelectScene()
    {
        SceneManager.LoadScene("SelectScene");
    }
    
    //////////////////RestartButton/////////////////////////
    public void RestartGame()
    {   
        // continue
        Time.timeScale = 1f;  

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    //////////////////QuitButton/////////////////////////
    public void QuitGame()
    {
        Application.Quit();
    }

}
