using UnityEngine;
using System.Collections;

public class EndGameFunctions : MonoBehaviour {

	public void quitApplication()
    {
        Application.Quit();
    }

    public void restartApplication()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
