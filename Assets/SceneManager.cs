using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Scene SelectLevelScene;

    public void PlayGame()
    {
        SceneManager.LoadScene(SelectLevelScene.name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
