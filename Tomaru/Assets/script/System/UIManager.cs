using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] GameObject cutScene;

    ///////////////////StartButton//////////////////
    public void GoToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    //public void GoToSelectScene()
    //{
    //    SceneManager.LoadScene("SelectScene");
    //}

    //////////////////QuitButton/////////////////////////
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoCutScene()
    {
        cutScene.SetActive(true);
    }

    public void EndCutScene()
    {
        cutScene.SetActive(false);
    }

    /////////////////////StageSelectButton///////////////
    //public void GoToStage1_1()
    //{
    //    SceneManager.LoadScene("Stage1-1"); 
    //}
    //public void GoToStage1_2()
    //{
    //    SceneManager.LoadScene("Stage1-2");
    //}
    //public void GoToStage1_3()
    //{
    //    SceneManager.LoadScene("Stage1-3");
    //}

}
