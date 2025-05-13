using Fusion.Photon.Realtime;
using Fusion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Collections.Unicode;

/*
public class SessionProperty
{
    public int sessionID;
}*/
public class FusionMatchmaking : MonoBehaviour
{

    [SerializeField] public NetworkRunner _runner;
    [SerializeField] private Text _playerListText;
    private SessionInfo _currentSession;
    private bool _isReady = false; 
    private Dictionary<PlayerRef, PlayerData> _playerDataCache = new Dictionary<PlayerRef, PlayerData>();
    public PlayerSpawner _spawner;
    public MainMenuUI mainmenu;

    private void Awake()
    {
        _runner = FindObjectOfType<NetworkRunner>();
        //_runner=GetComponent<NetworkRunner>();
    }
    private void Start()
    {
        // Initialize UI
       /* _createButton.onClick.AddListener(CreateSession);
        _joinButton.onClick.AddListener(JoinSession);
        _readyButton.onClick.AddListener(ToggleReady);
        _startButton.onClick.AddListener(StartGame);
        _startButton.interactable = false;*/ // Disable start button initially
    }

    public async void CreateSession()
    {
        /*var customProps = new Dictionary<string, SessionProperty>() {
    { "SessionID",  mainmenu.input.text}};
*/
        // Start a new session in Shared Mode

        var result = await _runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName=mainmenu.input.text,
            PlayerCount = 2,
            //SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
            //CustomLobbyName = mainmenu.input.text,
            //SessionProperties = customProps,
           // Scene = SceneRef.FromIndex(1)
        }) ;

        if (result.Ok)
        {
            //Debug.Log("Session created successfully!");
            //_runner.AddCallbacks(new NetworkCallbacks(this));
            /*_createButton.interactable = false;
            _joinButton.interactable = false;
            _startButton.interactable = true; */// Enable start button for host
            //_runner.LoadScene(SceneRef.FromIndex(1), LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("Failed to create session: " + result.ErrorMessage);
        }
    }

    public async void JoinSession()
    {
       /* var result = await _runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Client,
            PlayerCount = 2,
           // CustomLobbyName= mainmenu.input.text,   
        });*/
            // Join a random session
        var result = await _runner.JoinSessionLobby(SessionLobby.Shared/*, mainmenu.input.text*/ );

        if (result.Ok)
        {
           // _runner.LoadScene(SceneRef.FromIndex(1), LoadSceneMode.Single);
            Debug.Log("Joined session successfully!" + _runner.ActivePlayers);
            foreach (var player in _runner.ActivePlayers)
            {
                Debug.Log("Player:" + player.PlayerId);
            }
                /* _createButton.interactable = false;
                 _joinButton.interactable = false;*/
                _runner.AddCallbacks(new NetworkCallbacks(this));
        }
        else
        {
            Debug.LogError("Failed to join session: " + result.ErrorMessage);
        }
    }
    public void StartGame()
    {

        _runner.LoadScene(SceneRef.FromIndex(1), LoadSceneMode.Single);
      
       /* if (_runner.IsServer && AreAllPlayersReady())
        {
            // Transition to gameplay scene
            _runner.SessionInfo.IsOpen = false; // Close session to prevent new players
            _runner.LoadScene(SceneRef.FromIndex(1), LoadSceneMode.Single);
        }*/
    }
    
    private void ToggleReady()
    {
        _isReady = !_isReady;
        //_readyButton.GetComponentInChildren<Text>().text = _isReady ? "Unready" : "Ready";

        // Notify the network of readiness state
        //_runner.SetPlayerData(_runner.LocalPlayer, new NetworkDictionary<string, NetworkString<_16>> { { "Ready", _isReady ? "True" : "False" } });

        UpdatePlayerList();
    }

    
    public void MultiplayerMatchmakingScreen()
    {
        mainmenu.lobby.SetActive(true);
        mainmenu.mainMenu.SetActive(false);
    }
    public async void SinglePlayer()
    {
        var result = await _runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Single,
           // SessionName = mainmenu.input.text,
            PlayerCount = 1,
            //SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
            //CustomLobbyName = mainmenu.input.text,
            //SessionProperties = customProps,
            // Scene = SceneRef.FromIndex(1)
        });

        if (result.Ok)
        {
            _runner.LoadScene(SceneRef.FromIndex(1), LoadSceneMode.Single);
            //Debug.Log("Session created successfully!");
            //_runner.AddCallbacks(new NetworkCallbacks(this));
            /*_createButton.interactable = false;
            _joinButton.interactable = false;
            _startButton.interactable = true; */// Enable start button for host
            //_runner.LoadScene(SceneRef.FromIndex(1), LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("Failed to create session: " + result.ErrorMessage);
        }

    }

    private bool AreAllPlayersReady()
    {
        foreach (var player in _runner.ActivePlayers)
        {
            if (_runner.TryGetPlayerObject(player, out NetworkObject playerObj))
            {
                var playerData = playerObj.GetComponent<PlayerData>();
                if (!playerData.IsReady)
                    return false;
            }
            else
                return false; // Player object not found
        }
        return true;
    }

    public void UpdatePlayerList()
    {
        string playerList = "Players:\n";
        _playerDataCache.Clear();

        foreach (var player in _runner.ActivePlayers)
        {
            if (_runner.TryGetPlayerObject(player, out NetworkObject playerObj))
            {
                var playerData = playerObj.GetComponent<PlayerData>();
                //_playerDataCache[player] = playerData;
                playerList += $"{playerData}:" /* {(playerData.IsReady ? "Ready" : "Not Ready")}\n"*/;
                Debug.Log("PlayerList"+ playerObj.name);
            }
        }

       // _startButton.interactable = _runner.IsServer && AreAllPlayersReady();
    }

    private void OnDestroy()
    {
       /* _createButton.onClick.RemoveAllListeners();
        _joinButton.onClick.RemoveAllListeners();
        _readyButton.onClick.RemoveAllListeners();
        _startButton.onClick.RemoveAllListeners();*/
    }
}
