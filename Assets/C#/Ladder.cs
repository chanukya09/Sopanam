using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Transform StartPoint, Endpoint;
    public CharacterController player;
    public void OnTeleport()
    {
        Debug.Log("Teleported"+ Endpoint.transform.localPosition+"global position:"+ Endpoint.transform.position);
        player.enabled = false;
        player.gameObject.transform.position=Endpoint.transform.position;
        player.enabled = true;
    }

}
