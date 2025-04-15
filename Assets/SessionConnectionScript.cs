using UnityEngine;
using Unity.Services.Multiplayer;
using System.Data;
using Unity.Netcode;
using Unity.Services.Core;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using System;

public class SessionConnectionScript : MonoBehaviour
{
    private string playerName;
    public int playerCount;
    private int maxPlayers = 4;
    private string lobbyName;

    private ConnectionState state = ConnectionState.Disconnected;
    private ISession session;
    private NetworkManager NetworkManager;
    public PlayerManagerScript playerManager;

    private enum ConnectionState
    {
        Disconnected,
        Connecting,
        Connected,
        Teardown
    }

    private async void Awake()
    {
        NetworkManager = GetComponent<NetworkManager>();
        NetworkManager.OnClientConnectedCallback += OnClientConnection;
        NetworkManager.OnSessionOwnerPromoted += OnPartyLeaderPromotion;
        await UnityServices.InitializeAsync();
    }

    private void OnGUI()
    {
        if (state == ConnectionState.Teardown)
        {
            GUI.enabled = false;
            return;
        }

        if (state == ConnectionState.Connected)
            return;

        GUI.enabled = state != ConnectionState.Connecting;
        using (new GUILayout.HorizontalScope(GUILayout.Width(250)))
        {
            GUILayout.Label("Profile Name", GUILayout.Width(100));
            playerName = GUILayout.TextField(playerName);
        }

        using (new GUILayout.HorizontalScope(GUILayout.Width(250)))
        {
            GUILayout.Label("Session Name", GUILayout.Width(100));
            lobbyName = GUILayout.TextField(lobbyName);
        }

        GUI.enabled = GUI.enabled && !string.IsNullOrEmpty(playerName) && !string.IsNullOrEmpty(lobbyName);

        if (GUILayout.Button("Host / Join a Lobby"))
        {
            HostorJoinSession();
        }
    }

    private void OnClientConnection(ulong clientId)
    {
        if (NetworkManager.LocalClientId == clientId)
        {
            Debug.Log($"Client-{clientId} is connected");
            playerManager.listPlayer(playerName);
        }
    }

    private void OnPartyLeaderPromotion(ulong sessionOwnerChange)
    {
        if (NetworkManager.LocalClient.IsSessionOwner)
        {
            Debug.Log($"Client-{NetworkManager.LocalClientId} is the party leader");
            playerManager.listPlayer(playerName);
        }
    }

    private async void HostorJoinSession()
    {
        state = ConnectionState.Connecting;

        try
        {
            AuthenticationService.Instance.SwitchProfile(playerName);
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            var options = new SessionOptions()
            {
                Name = lobbyName,
                MaxPlayers = maxPlayers
            }.WithDistributedAuthorityNetwork();

            session = await MultiplayerService.Instance.CreateOrJoinSessionAsync(lobbyName, options);
            state = ConnectionState.Connected;
        }
        catch (Exception e)
        {
            state = ConnectionState.Disconnected;
            Debug.LogException(e);
        }
    }

    public void Disconnect()
    {
        state = ConnectionState.Teardown;
    }

    private void OnDestroy()
    {
        session?.LeaveAsync();
    }
}
