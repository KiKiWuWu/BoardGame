using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider ProgressBar;

    private AsyncOperation asyncOperation;


    //Loads the main game scene
    public void loadMainLevel()
    {
        StartCoroutine(LoadLevelAsync("MainGameScene"));
    }


    //Loads a level async with the given name of the scene
    private IEnumerator LoadLevelAsync(string nameOfLevel)
    {
        LoadingScreen.SetActive(true);
        yield return new WaitForSeconds(1f);

        asyncOperation = SceneManager.LoadSceneAsync(nameOfLevel, LoadSceneMode.Single);

        while (!asyncOperation.isDone)
        {
            ProgressBar.value = asyncOperation.progress;

            yield return null;
        }
    }


    //Loads the tutorial game scene
    public void loadTutorialLevel()
    {
        StartCoroutine(LoadLevelAsync("TutorialScene"));
    }


    //Closes the application (on mobile devices)
    public void quitApplication()
    {
        Application.Quit();
    }


    //Loads the main menu scene
    public void returnToMainMenu()
    {
        StartCoroutine(LoadLevelAsync("MainMenu"));
    }
}