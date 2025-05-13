using UnityEngine;

public class TheSoul : MonoBehaviour, IInteractable
{
    public UIManager uiManager;
    public void Interact()
    {
        uiManager.EnableGameOver();
    }
}
