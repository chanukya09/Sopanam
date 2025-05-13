using Fusion;
using Photon.Realtime;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined,ISceneLoadDone
{
    [SerializeField] private NetworkPrefabRef playerPrefab;
    [SerializeField] FusionMatchmaking fusionMatchmaking;
    public NetworkRunner NetworkRunner;
    //public Transform spawnLocation;
    Vector3 position;
    PlayerRef playerRef;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        position = new Vector3(-43.8f,1,0);
    }
    private void Awake()
    {
        fusionMatchmaking = FindAnyObjectByType<FusionMatchmaking>();
    }
    public void PlayerJoined(PlayerRef player)
    {
        Debug.LogError("PlayerJoined:"+player.ToString() +" LocalPlayer:"+Runner.LocalPlayer);
        if (Runner.GameMode == GameMode.Single)
        {
            playerRef = player;
            
        }
        if (player == Runner.LocalPlayer)
        {
            playerRef= player;
        }
    }

    public void SceneLoadDone(in SceneLoadDoneArgs sceneInfo)
    {
        if (Runner.GameMode == GameMode.Single)
        {
            Invoke("SpawnPlayer", 1);
        }
        else
        {
            SpawnPlayer();
        }
       
    }

    void SpawnPlayer()
    {
        NetworkObject playerObject = Runner.Spawn(playerPrefab, position, Quaternion.identity, playerRef);
        DiceManager.instance.player = playerObject.GetComponent<Player>();
        if (playerObject != null)
        {
            Debug.Log($"Player spawned: InputAuthority={playerObject.InputAuthority}, StateAuthority={playerObject.StateAuthority}");
        }
        else
        {
            Debug.LogError("Failed to spawn player object.");
        }
    }
}