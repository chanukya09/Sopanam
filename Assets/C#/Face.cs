using UnityEngine;

public class Face:MonoBehaviour
{
    public DiceRoll dice;
    public int faceNo;
    private void Awake()
    {
        dice = GetComponentInParent<DiceRoll>();
    }
  
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("DiceValue:" + faceNo+"DiceMagnitude:"+ dice.rb.angularVelocity.magnitude);
        if (dice.rb.angularVelocity.magnitude == 0)
        {
            dice.diceValue = faceNo;
            Debug.Log("DiceValue:" + faceNo);
            dice.uiManager.diceTxt.GetComponent<Animator>().Play("DiceResult");
            dice.uiManager.diceTxt.text=dice.diceValue.ToString();
            dice.IsAdded = false;
            dice.rb.useGravity = false;
            dice.SetActive(false);


            //DiceManager.AddDiceValue?.Invoke();
        }
    }
}