using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private CharacterController _controller;

    [SerializeField]
    private Camera _followCamera;

    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public GameObject PausePanel;


    public float lookSpeed = 2f;
    public float lookXLimit = 45f;


    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;
    static public bool dialogueState = false;
    static string dialogue_NPC = "NONE";

    private StartGame intro;

    private HappinessManager manager;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    
        manager = FindAnyObjectByType<HappinessManager>();

        PausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        intro = FindObjectOfType<StartGame>();
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

        if (dialogueState == false && Time.timeScale == 1)
        {
            if (_controller.transform.position.y < -20)
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
            if (Input.GetButton("Jump") && canMove && _controller.isGrounded)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!_controller.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }


            // Handles Rotation
            _controller.Move(moveDirection * Time.deltaTime);

            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                _followCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }
    }
}
