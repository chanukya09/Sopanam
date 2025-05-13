using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DiceManager : MonoBehaviour
{       

    public static DiceManager instance; 
    public Player player;
    public DiceRoll dice1,dice2;
    public int diceValue;
    public static UnityAction<int> doorUnlock;
    public int unlockedDoorNo;
    public bool Unlocked;
    public Transform dice1Pos, dice2Pos;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (!dice1.IsAdded /*&& !dice2.IsAdded*/)
        {
            AddDice();
        }
      /*  if(Input.GetKeyDown(KeyCode.C))
        {
            dice1.transform.position = dice1Pos.position;
           // dice2.transform.position = dice2Pos.position;
        }*/
    }

    void AddDice()
    {
        /* if (dice1==null)
         {
             diceValue=dice2.diceValue;
             dice2.IsAdded = true;
         }
         else if(dice2==null)
         {
             diceValue = dice1.diceValue; 
             dice1.IsAdded = true;
         }*/

        /*if(dice1!=null&&dice2!=null)
        {*/
        if (!Unlocked)
        {
            if (true/*dice1.diceValue == 6 *//*|| dice2.diceValue == 6*/)
            {
                Unlocked = true;
                return;
            }
        }
        diceValue = dice1.diceValue/* + dice2.diceValue*/;
        dice1.IsAdded = true;
       // dice2.IsAdded = true;
        if (Unlocked)
        {   
            unlockedDoorNo = player.presentRoomNo+diceValue;
            player.RoomNoCheck();
            for (int i = player.presentRoomNo; i < unlockedDoorNo ; i++)
            {
                //Debug.Log("Room Unlock"+i);
                doorUnlock?.Invoke(i);
            }
        }
    }


}
