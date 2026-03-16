using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public PauseManager pauseManager;

    //////////////////ContinueButton/////////////////////////
    public void ContinueButton()
    {
        pauseManager.ResumeGame();
    }
    
    
    //////////////////RestartButton/////////////////////////
    public void GoToTitleScene()
    {   
        // continue
        Time.timeScale = 1f;  

        SceneManager.LoadScene("TitleScene");

    }
    //////////////////QuitButton/////////////////////////
    public void QuitGame()
    {
        Application.Quit();
    }

}
