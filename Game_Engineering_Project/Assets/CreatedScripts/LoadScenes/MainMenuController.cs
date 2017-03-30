using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    //Loads the main game scene
	public void loadMainLevel()
    {
        SceneManager.LoadScene("MainGameScene", LoadSceneMode.Single);
    }


    //Loads the tutorial game scene
    public void loadTutorialLevel()
    {
        SceneManager.LoadScene("TutorialScene", LoadSceneMode.Single);
    }


    //Closes the application (on mobile devices)
    public void quitApplication()
    {
        Application.Quit();
    }


    //Loads the main menu scene
    public void returnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
