using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.Unicode;

public class NetworkCallbacks : INetworkRunnerCallbacks
{
    private FusionMatchmaking _sessionManager;
    PlayerRef _playerRef;
    public PlayerSpawner _playerSpawner;
    public NetworkCallbacks(FusionMatchmaking sessionManager)
    {
        _sessionManager = sessionManager;
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
         Debug.Log($"Player {player.PlayerId} joined the session!");

        if (player == runner.LocalPlayer)
        {
            _playerRef = player;
        }
        //_sessionManager.UpdatePlayerList();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"Player {player.PlayerId} left the session!");
        //_sessionManager.UpdatePlayerList();
    }

    // Implement other required callbacks (empty for this example)
    public void OnInput(NetworkRunner runner, NetworkInput input) {
      //  Debug.LogError("Network Input " + runner.GetPlayerUserId());
    }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) {
        Debug.Log("ConnectedToServer");
        //Check is ready
    }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) {
        Debug.Log("SimulationMessage"+message.ToString());
    }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) {
        runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = _sessionManager.mainmenu.input.text,
            PlayerCount = 2,
            //SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
            //CustomLobbyName = mainmenu.input.text,
            // ...
        });
        Debug.Log("SessionUpdated" + sessionList.Count);
    }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, System.ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) {

    }

    public void OnSceneLoadStart(NetworkRunner runner) { }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        Debug.Log("ObjectEnter");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }
}