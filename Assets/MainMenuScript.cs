using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void GameStart() => SceneManager.LoadScene("SinglePlayerPipes");

    public void LobbyStart() => SceneManager.LoadScene("ServerList");
    public void MultiplayerStart() => SceneManager.LoadScene("MultiplayerPipes");

    public void ReturnToMenu() => SceneManager.LoadScene("MainMenu");


    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
