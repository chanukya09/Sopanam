
using Fusion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DiceRoll : NetworkBehaviour
{
    //public static DiceRoll instance;
    public UIManager uiManager;
    [HideInInspector]public Rigidbody rb;
    public float randomForceValue;
    public float force;

   // public Face face;
    public int diceValue;
    public bool IsAdded,IsRollable;
    public List<GameObject> faces;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    /*  private void Awake()
      {
          instance = this;
      }
  */
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsRollable)
        {
            uiManager.DisableDiceRoll();
            IsRollable = false;
            rb.useGravity = true;
            SetActive(true);
            force = Random.Range(400, randomForceValue);
            rb.AddForce(Vector3.up*2000/*,ForceMode.Impulse*/);
            rb.AddTorque(force, force, force);
            // Invoke("Torque",.2f);
        }
    }
    
    
 /*   void NetworkRoll()
    {
        var getInput = GetInput<NetworkInputData>(out var input);
        if (getInput)
        {
            uiManager.DisableDiceRoll();
            IsRollable = false;
            rb.useGravity = true;
            SetActive(true);
            force = Random.Range(400, randomForceValue);
            rb.AddForce(Vector3.up * 2000*//*,ForceMode.Impulse*//*);
            rb.AddTorque(force, force, force);
        }
    }*/

    public void SetActive(bool active )
    {
        foreach(var go in faces)
        {
            go.SetActive(active); 
        }
    }
    void Torque()
    {
        rb.AddTorque(force,force,force);    
    }
}
