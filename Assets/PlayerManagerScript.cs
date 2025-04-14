using UnityEngine;
using UnityEngine.UI;

public class PlayerManagerScript : MonoBehaviour
{
    public Text playerList;
    //ivate List<string> connectedPlayers;

    public void listPlayer(string playerName)
    {
        playerList.text += "\n" + playerName;
    }
}
