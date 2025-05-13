using System.Collections.Generic;
using UnityEngine;

public class PopulateObjects : MonoBehaviour
{
    public List<Room> rooms;
    public List<Gate> gates;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i=0; i<rooms.Count; i++)
        {
            rooms[i].roomNo=i;
        }
        for (int i = 0; i < gates.Count; i++)
        {
            gates[i].gateNo = i;
            Debug.Log(gates[i].gateNo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
