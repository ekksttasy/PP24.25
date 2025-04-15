using UnityEngine;
using UnityEngine.UI;

public class PlayerManagerScript : MonoBehaviour
{
    public Text playerList;

    public void listPlayers(string playerName)
    {
        playerList.text += playerName + "\n";
    }

    public void removePlayer(string playerName)
    {
        string[] players = playerList.text.Split('\n');
        playerList.text = string.Join("\n", System.Array.FindAll(players, p => p != playerName && !string.IsNullOrEmpty(p)));
    }
}
