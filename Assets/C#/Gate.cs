
using UnityEngine;

public class Gate : MonoBehaviour
{   
    public Animator animator;
    public string GateUnlock,GateLock;
    public int gateNo;
    public bool unlockGate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        DiceManager.doorUnlock += DoorUnlock;
    }
    private void OnDisable()
    {
        DiceManager.doorUnlock -= DoorUnlock;
    }
 
    void DoorUnlock(int doorNo)
    {
        if (doorNo == gateNo)
        {   
            unlockGate = true;
            animator.Play(GateUnlock);
            if (GetComponent<Collider>())
            {
                GetComponent<Collider>().isTrigger = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player");
            Player.instance.RoomNoCheck();
            if (gateNo< Player.instance.presentRoomNo  )
            {
                animator.Play(GateLock);
                unlockGate = false;
                GetComponent<Collider>().isTrigger = false;
                //Close The Door
                //animator.Play()
            }

            //GetComponent<Collider>().isTrigger = false;
        }
    }
    Vector3 direction;
    // Update is called once per frame
   /* void Update()
    {
        if (unlockGate)
        {
            direction = (transform.position - endPosition).normalized;
            Debug.Log("Direction" + direction);
            transform.Translate(direction * Time.deltaTime * 2);

            if (direction.magnitude == 0)
            {
                unlockGate = false;
            }*//*
            timeElapsed += .5f * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(transform.position.z, transform.position.z + 2, timeElapsed));
            interpolation = timeElapsed / 1;
            transform.position = Vector3.Slerp(startpoint.position, endpoint.position, interpolation);
            //timeElapsed=( timeElapsed+1)%(interpolationFrame+1);
            Debug.Log("Frames" + timeElapsed + "interpolation" + interpolation);
            if (timeElapsed > 1)
            {
                unlockGate = false;
                timeElapsed = 0;
            }*//*
        }
    }*/
}
