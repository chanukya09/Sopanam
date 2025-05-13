using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public void Interact()
    {   
        this.gameObject.SetActive(false);
    }
}
