using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPersonController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public GameObject PausePanel;

    public bool isFirstPersonView = true;
    public Transform Targetposition;
    public int speed = 1;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    //private Vector3 firstPersonPosition = new Vector3(2, 0, 2);
    //private Quaternion firstPersonRotation = Quaternion.Euler(0, 0, 0);

    //private Vector3 thirdPersonPosition = new Vector3(0, 14, -14);
    //private Quaternion thirdPersonRotation = Quaternion.Euler(55, 0, 15);

    private Vector3 firstPersonOffset = new Vector3(2, 0, 2); // Adjust as needed
    private Vector3 thirdPersonOffset = new Vector3(0, 14, -14); // Adjust as needed

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;
    static public bool dialogueState = false;
    static string dialogue_NPC = "NONE";

    private StartGame intro;

    private HappinessManager manager;


    CharacterController characterController;
    void Start()
    {
        manager = FindAnyObjectByType<HappinessManager>();

        PausePanel.SetActive(false);
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        intro = FindObjectOfType<StartGame>();

        Targetposition.position = transform.position + transform.TransformDirection(firstPersonOffset);
        Targetposition.rotation = transform.rotation;
    }

    public void changeDialogueState(bool value, string NPC)
    {
        dialogueState = value;
        dialogue_NPC = NPC;
    }

    public bool getDialogueState()
    {
        return dialogueState;
    }

    public string getDialogueNPC()
    {
        return dialogue_NPC;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            PausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleView();
        }

        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, Targetposition.position, speed * Time.deltaTime);
        playerCamera.transform.rotation = Quaternion.Lerp(playerCamera.transform.rotation, Targetposition.rotation, speed * Time.deltaTime);

        if (dialogueState == false && Time.timeScale == 1)
        {
            if(characterController.transform.position.y < -20)
            {
                manager.end_game(3);
                SceneManager.LoadScene("EndGame");
            }

            //Handles Movement
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            //Handles Jumping
            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }


            // Handles Rotation
            characterController.Move(moveDirection * Time.deltaTime);

            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }
    }

    private void ToggleView()
    {
        if (isFirstPersonView)
        {
            Targetposition.position = transform.position + transform.TransformDirection(thirdPersonOffset);
            Targetposition.rotation = Quaternion.LookRotation(transform.position - Targetposition.position);
        }
        else
        {
            Targetposition.position = transform.position + transform.TransformDirection(firstPersonOffset);
            Targetposition.rotation = transform.rotation;
        }

        isFirstPersonView = !isFirstPersonView;
    }
}
