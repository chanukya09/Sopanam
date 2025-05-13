using Fusion;

public class PlayerData:NetworkBehaviour
{
    [Networked] public NetworkBool IsReady { get; set; }
    [Networked, Capacity(16)] public NetworkString<_16> Nickname { get; set; }

    public override void Spawned()
    {
        // Set default values when the player object is spawned
        if (Object.HasInputAuthority)
        {
            Nickname = $"Player_{Runner.LocalPlayer.PlayerId}";
        }
    }
}
