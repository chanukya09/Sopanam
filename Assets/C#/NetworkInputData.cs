using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public Vector2 MoveInput; // Movement input (WASD/analog stick)
    public Vector2 LookInput; // Mouse/camera rotation input
    public float moveMagnitude;
    public NetworkBool JumpPressed; // Jump button state
    public NetworkBool ActionPressed;
    public NetworkBool diceRoll; // Action button state (e.g., shoot)
}