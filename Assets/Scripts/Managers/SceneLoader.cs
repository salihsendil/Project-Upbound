using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneIndex {
    MainMenu,
    GameScene
}

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene((int)SceneIndex.MainMenu);
    }

    public void LoadOptionsScene()
    {
        //on the future
        //add enum to SceneIndex
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene((int)SceneIndex.GameScene);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

}
