using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public void loadMainLevel()
    {
        SceneManager.LoadScene("MainGameScene", LoadSceneMode.Single);
    }

    public void loadTutorialLevel()
    {
        SceneManager.LoadScene("TutorialScene", LoadSceneMode.Single);
    }

    public void quitApplication()
    {
        Application.Quit();
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
