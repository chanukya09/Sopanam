using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputProvider : SimulationBehaviour, INetworkRunnerCallbacks
{
    private InputSystem_Actions _inputActions;
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private bool _jumpPressed;
    private bool _actionPressed;
    private bool _diceRoll;
    public NetworkRunner _runner;
    public NetworkInputData data;
    public void OnEnable()
    {
        _runner = FindObjectOfType<NetworkRunner>();
        //Debug.Log($"NetworkRunner found. ProvideInput={_runner.ProvideInput}, GameMode={_runner.GameMode}");
        if (_runner != null)
        {
            _runner.ProvideInput = true;
            _runner.AddCallbacks(this);
        }
    }
    private void Awake()
    {
        // Initialize the Input System
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.Enable();

        // Bind input actions
        _inputActions.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _inputActions.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
        _inputActions.Player.Look.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
        _inputActions.Player.Look.canceled += ctx => _lookInput = Vector2.zero;
        _inputActions.Player.Jump.performed += ctx => _jumpPressed = true;
       
        //  _inputActions.Player.Action.performed += ctx => _actionPressed = true;
    }
    
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        /*if (runner != null || runner.IsRunning)
        {
            
            Debug.LogWarning("Cannot provide input: Runner is not running or no valid local player.");
            return;
        }*/
        // Debug.LogError("Network Input"+runner.GetPlayerUserId());
        data = new NetworkInputData
        {
            MoveInput = _moveInput,
            LookInput = _lookInput,
            JumpPressed = _jumpPressed,
            ActionPressed = _actionPressed,
            diceRoll = _diceRoll,
            moveMagnitude = _moveInput.magnitude,
    };

        input.Set(data);

        // Reset one-time inputs
        _jumpPressed = false;
        _actionPressed = false;
    }
    private void Update()
    {
    }
    private void OnDestroy()
    {
        // Clean up input bindings
        _inputActions.Player.Disable();
        _inputActions.Player.Move.performed -= ctx => _moveInput = ctx.ReadValue<Vector2>();
        _inputActions.Player.Move.canceled -= ctx => _moveInput = Vector2.zero;
        _inputActions.Player.Look.performed -= ctx => _lookInput = ctx.ReadValue<Vector2>();
        _inputActions.Player.Look.canceled -= ctx => _lookInput = Vector2.zero;
        _inputActions.Player.Jump.performed -= ctx => _jumpPressed = true; 
      
        // _inputActions.Player.Action.performed -= ctx => _actionPressed = true;
    }
    private void OnDisable()
    {
        if (_runner != null)
        {
            _runner.RemoveCallbacks(this);
        }
    }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }
}