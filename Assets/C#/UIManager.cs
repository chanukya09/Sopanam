using TMPro;
using UnityEngine;

public class UIManager:MonoBehaviour
{
    public DataBase db;
    public TextMeshProUGUI diceTxt;
    public TextMeshProUGUI CollectableScore;
    public TextMeshProUGUI Timer;
    public TextMeshProUGUI GameComplete;

    public TextMeshProUGUI RollDice;
    public void AddUpdateScore()
    {
        db.AddScore();
        CollectableScore.text = "Score:"+ db.colletible.ToString();
    }

    public void EnableGameOver()
    {
        GameComplete.gameObject.SetActive(true);    
    }

    public void EnableDiceRoll()
    {
        RollDice.gameObject.SetActive(true);
    }
    public void DisableDiceRoll()
    {
        RollDice.gameObject.SetActive(false);
    }
    public void UpdateTimer()
    {

    }

}