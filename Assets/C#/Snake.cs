using UnityEngine;

public class Snake : MonoBehaviour
{
    public Transform StartPoint, Endpoint;
    public Player player;
    public void OnTeleport()
    {
        player.gameObject.transform.position = Endpoint.transform.position;
    }
}
