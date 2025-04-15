using UnityEngine;
using UnityEngine.UI;

public class PlayerManagerScript : MonoBehaviour
{
    public Text playerList;

    public void listPlayers(string playerName)
    {
        playerList.text += playerName + "\n";
    }
}
