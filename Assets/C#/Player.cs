using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player : NetworkBehaviour
{
    public static Player instance;

    public DiceManager diceManager;
    public UIManager uiManager;

    public GameObject cameraRotation;
    public Animator animator;
    private Vector2 currentMovement, look;
    public float rotX, rotY;
    
    //[SerializeField] private float sensitivityX, sensitivityY, rotationSpeed;
    private Vector3 lookDirection, moveDirection;

    private CharacterController playerController;
    Vector3 gravityVector;
    float veritcleVelocity; float gravity = 9.81f;
    public int speed;
    public RaycastHit hit;

    public GameObject room;
    public int presentRoomNo,unlockedRoomNo;
    float rotationSpeed;
    public Transform raycast;
    public IInteractable interactableObject;

    public bool movementPressed;
    private bool running;
    public float movementMagnitude;

    Quaternion requiredRotation;
   // InputProvider inputProvider;
    public bool runPressed;
    private void Awake()
    {
        instance = this;
        diceManager = FindAnyObjectByType<DiceManager>();
        uiManager = FindAnyObjectByType<UIManager>(); 
        playerController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cameraRotation = FindAnyObjectByType/* GetComponent*/<Camera>().gameObject;
        //inputProvider=FindAnyObjectByType<InputProvider>(); 
    }
    private void  Start()
    {
       /* movementMagnitude = 0;
        rotationSpeed = 1040;
        speed = 4;
        gravityVector = new Vector3(0, Gravity(), 0);
        this.transform.position = new Vector3(-43, 0, 0);*/
    }
   
    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            movementMagnitude = 0;
            rotationSpeed = 1040;
            speed = 4;
            gravityVector = new Vector3(0, Gravity(), 0);
            this.transform.position=new Vector3(-43,0,0); 
            //Debug.Log("input" + GetInput<NetworkInputData>(out var input));
        }
    }

    private void Update()
    {
        //Movement();
        // LookMovement();
    }
    public override void FixedUpdateNetwork()
    {
        NetworkedMovement();
       // NormalMovement();
}

    void NetworkedMovement()
    {
        var getInput = GetInput<NetworkInputData>(out var input);
        if (getInput)
        {
            animator.SetFloat("Speed", input.moveMagnitude);
            // Calculate the desired direction relative to the camera
            moveDirection = new Vector3(input.MoveInput.x, 0,input.MoveInput.y);
            moveDirection = Quaternion.AngleAxis(cameraRotation.transform.rotation.eulerAngles.y, Vector3.up) * moveDirection;
            moveDirection.Normalize();
            if (moveDirection != Vector3.zero)
            {
                requiredRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, requiredRotation, rotationSpeed * Runner.DeltaTime);
                //Debug.Log("TransformRotation"+transform.rotation);
            }
            playerController.Move(moveDirection * speed * Runner.DeltaTime);

        }
    }
    void NormalMovement()
    {
        animator.SetFloat("Speed", movementMagnitude);
        // Calculate the desired direction relative to the camera
        moveDirection = new Vector3(currentMovement.x, 0, currentMovement.y);
        moveDirection = Quaternion.AngleAxis(cameraRotation.transform.rotation.eulerAngles.y, Vector3.up) * moveDirection;
        moveDirection.Normalize();
        if (moveDirection != Vector3.zero)
        {
            requiredRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, requiredRotation, rotationSpeed * Time.deltaTime /*Runner.DeltaTime*/);
        }
        playerController.Move(moveDirection * speed * Time.deltaTime);
    }
    public void Action(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            interactableObject.Interact();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            interactableObject = other.GetComponent<IInteractable>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (interactableObject != null)
        {
            interactableObject = null;
        }
    }
    public void RoomNoCheck()
    {   

        var pos=transform.position;
        Physics.Raycast(pos, Vector3.down , out hit, 10);
        {
            Debug.Log("Hit" +hit);
            Debug.Log("Hit transform" +hit.transform);
            Debug.Log("Hit gameobject" + hit.transform.gameObject);

            room = hit.transform.gameObject;
            presentRoomNo = hit.transform.GetComponent<Room>().roomNo;
            if (presentRoomNo == diceManager.unlockedDoorNo)
            {
                Debug.Log("Room" + room.name);
                diceManager.dice1.transform.position = diceManager.dice1Pos.position;
                
                diceManager.dice1.IsRollable=true;
                uiManager.EnableDiceRoll();
                if (hit.transform.GetComponent<Ladder>())
                {
                    Ladder ladder = hit.transform.GetComponent<Ladder>();
                    ladder.OnTeleport();
                }
                if (hit.transform.GetComponent<Snake>())
                {
                    Snake snake = hit.transform.GetComponent<Snake>();
                    snake.OnTeleport();
                }
            }
            //return room;    
        }
    }


    /* public void LookMovement()
    {
        rotY += look.x * Time.deltaTime * sensitivityY;
        //cameraRotation.transform.Rotate(Vector3.up * rotY);
        rotX -= look.y * Time.deltaTime * sensitivityX;
    }*/
    public void MoveCharacter(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            currentMovement = value.ReadValue<Vector2>();

            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
            if (!running)
            {
                movementMagnitude = (currentMovement.magnitude / 2);
            }
        }
        if (value.canceled)
        {
            currentMovement = value.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
            if (!running)
            {
                movementMagnitude = 0;
            }
        }
    }
    public void Roll(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            animator.Play("Roll");
        }
    }

    public void Movement()
    {
        animator.SetFloat("Speed",movementMagnitude); 
        // Calculate the desired direction relative to the camera
        moveDirection = new Vector3(currentMovement.x, 0, currentMovement.y); 
        moveDirection = Quaternion.AngleAxis(cameraRotation.transform.rotation.eulerAngles.y, Vector3.up) * moveDirection;
        moveDirection.Normalize();
        if (moveDirection != Vector3.zero)
        {
            requiredRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, requiredRotation, rotationSpeed * Time.deltaTime);
        }
        playerController.Move(moveDirection * speed * Time.deltaTime);
    }
    public void RunCharacter(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            movementMagnitude = currentMovement.magnitude;
            running = true;
            runPressed = value.ReadValueAsButton();
        }
        if (value.canceled)
        {
            runPressed = value.ReadValueAsButton();
            running = false;
            movementMagnitude = currentMovement.magnitude / 2;
        }
    }
    public float Gravity()
    {
        if (playerController.isGrounded)
        {
            veritcleVelocity = -1;
        }
        else
        {

            veritcleVelocity -= gravity;
        }
        return veritcleVelocity;
    }
}
