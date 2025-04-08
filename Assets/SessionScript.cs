using UnityEngine;
using Unity.Services.Multiplayer;

public class SessionScript : MonoBehaviour
{
    int playerCount;
    async void HostSession()
    {
        var options = new SessionOptions 
        {
            MaxPlayers = playerCount
        }.WithDistributedAuthorityNetwork;
        var session = await MultiplayerService.Instance.CreateSessionAsync(options);
    }
}
