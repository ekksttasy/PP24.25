using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void GameStart() => SceneManager.LoadScene("SinglePlayerPipes");

    public void LobbyStart() => SceneManager.LoadScene("ServerList");
    public void MultiplayerStart() => SceneManager.LoadScene("MultiplayerPipes");

    public void Restart()
    {

    }

    public void ReturnToMenu()
    {
        var _session = FindFirstObjectByType<SessionConnectionScript>();
        if (_session != null)
        {
            _session.Disconnect();
        }

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
    }
}
