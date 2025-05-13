using UnityEngine;

public class Collectible : MonoBehaviour,IInteractable
{
    public UIManager UIManager;
    
    public void Interact()
    {
        UIManager.AddUpdateScore();
        gameObject.SetActive(false);
    }

}
