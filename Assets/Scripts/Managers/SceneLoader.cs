using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneIndex {
    MainMenu,
    GameScene,
    OptionsScene
}

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene((int)SceneIndex.MainMenu);
    }

    public void LoadOptionsScene()
    {
        SceneManager.LoadScene((int)SceneIndex.OptionsScene);
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
