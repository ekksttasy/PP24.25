using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static birdScript;

public class MainMenuScript : MonoBehaviour
{
    public void GameStart() => SceneManager.LoadScene("SinglePlayerPipes");

    public void LobbyStart() => SceneManager.LoadScene("ServerList");
    public void MultiplayerStart()
    {
        // Ensures lobby exists to join & player is owner
        var _session = FindFirstObjectByType<SessionConnectionScript>();
        if (_session != null && _session.CurrentConnectionState == "Connected" && _session.IsOwner)
        {
            SceneManager.LoadScene("MultiplayerPipes");
            birdScript.ResetBirdPosition();
        }
        else
        {
            Debug.Log("No session found, join a lobby!");
        }
    }

    public void Restart()
    {
         if (SceneManager.GetActiveScene().name == "SinglePlayerPipes")
         {
            SceneManager.LoadScene("SinglePlayerPipes");
         }
         else if (SceneManager.GetActiveScene().name == "MultiplayerPipes")
         {
            SceneManager.LoadScene("MultiplayerPipes");
         }
    }

    public void ReturnToMenu()
    {
        var _session = FindFirstObjectByType<SessionConnectionScript>();
        if (_session != null)
        {   // Hides GUI
            _session.Disconnect();

            // Destroys SessionManager (?)
            Destroy(_session.gameObject);
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
