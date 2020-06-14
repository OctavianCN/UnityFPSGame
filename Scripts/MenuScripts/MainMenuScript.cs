using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    private int loadingSceneNr = 1;
    private int gameFirstSceneNr = 2;
    private static bool newGame = true;
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void NewGame()
    {
        MainMenuScript.newGame = true;
        LoadingScene.sceneToLoad = gameFirstSceneNr;
        SceneManager.LoadScene(loadingSceneNr);   
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Options()
    {
        this.transform.parent.Find("OptionsMenu").gameObject.SetActive(true);
    }
    public void Resume()
    {
        MainMenuScript.newGame = false;
        DataManager.data = SaveSystem.Load();
        LoadingScene.sceneToLoad = gameFirstSceneNr;
        SceneManager.LoadScene(loadingSceneNr);
    }
    public static bool GetNewGame()
    {
        return newGame;
    }
   
}
