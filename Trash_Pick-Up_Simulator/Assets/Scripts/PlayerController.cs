/*****************************************************************************
// File Name :         PlayerController.cs
// Author :            Lucas Johnson
// Creation Date :     August 31, 2022
//
// Brief Description : A C# script that handles the overal control of the
                       player including movement and looking around.
*****************************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region///////////// Public Variables: ///////////////

    // References
    [Header("Refrences:")]
    [Tooltip("Reference to the transform of the first person camera")]
    [SerializeField] Transform playerCamera = null;


    // Movement
    [Header("Movement:")]
    [Tooltip("Controls the speed at which the player looks around with the mouse")]
    [SerializeField] public float mouseSensitivity = 3.5f;
    [Tooltip("The speed at which the player moves")]
    [SerializeField] float walkSpeed = 6f;
    public float jumpHeight = 1.0f;
    [Tooltip("The speed at which the player moves towards the ground")]
    [SerializeField] float gravity = -13f;
    [Tooltip("The amount of smoothing when the player starts and stops moving")]
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [Tooltip("The amount of smoothing when the player starts and stops looking around with the mouse")]
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    // Cursor
    [Header("Cursor:")]
    [Tooltip("Whether or not the mouse is hidden and locked to the center of the screen")]
    [SerializeField] public bool lockCursor = true;

    // Raycast Detection
    [Header("Raycast Detection:")]
    [Tooltip("Distance to raycast from camera")]
    [SerializeField][Range(1f, 3f)] public float hitRange = 2f;

    // Throw Ability
    [Header("Throw Ability:")]
    [Tooltip("Force at which an object is thrown")]
    [SerializeField][Range(500f, 1500f)] public float maxThrowForce = 1000f;
    #endregion

    #region///////////// Private Variables: ///////////////

    // Refrences
    CharacterController controller = null;
    Transform holdPoint;
    Transform firePoint;

    // Movement
    float cameraPitch = 0f;
    Vector3 moveVelocity;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;
    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;
    bool grounded;


    // Raycast
    int playerMask = ~(1 << 8); // Hides the player object from the raycast
    #endregion

    // PowerBar
    public Slider powerbarSlider;
    public Image sliderFillImage;
    
    [Range(0.01f, 1f)]
    public float fillRate = 0.25f;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Get Refrences
        controller = GetComponent<CharacterController>();
        holdPoint = GameObject.Find("Holding Point").transform;
        firePoint = GameObject.Find("Fire Point").transform;

        // Lock cursor and hide it during gameplay
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        //mouseSensitivity = PlayerPrefs.GetFloat("SensitivityPref");

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Call Movement updates
        UpdateMouseLook();
        UpdateMovement();

        // Call Ability Updates
        HandlePickup();
        HandleThrowing();
    }

    /// <summary>
    /// Handles looking around using the mouse position
    /// </summary>
    void UpdateMouseLook()
    {
        // Get Mouse Input, smooth, and adjust it to match chosen sensitivity
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);
        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        
        // Restrict camera from moving past straight up & down
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        // Update camera and player rotation based on input
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    /// <summary>
    /// Handles moving the player around using the WASD keys
    /// </summary>
    void UpdateMovement()
    {
        if(controller.isGrounded)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        // Handle if the player is falling
        if (grounded && moveVelocity.y < 0f)
        {
            moveVelocity.y = -2f;
        }

        // Get Movement input and smooth it
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        // Calculate velocity based on set movement speed and apply it to the player
        Vector3 move = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed;
        controller.Move(move * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            moveVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
        moveVelocity.y += gravity * Time.deltaTime;

        controller.Move(moveVelocity * Time.deltaTime);
    }

    /// <summary>
    /// Handles the ability to pick up objects
    /// </summary>
    void HandlePickup()
    {
        // Draw a line in the inspector to show pickup range
        Debug.DrawRay(playerCamera.position, playerCamera.forward * hitRange, Color.yellow);

        // Continue if the player is not holding anything
        if (holdPoint.childCount != 0)
        {
            return;
        }

        // If input, check if the player is looking at an object that can be picked up
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hitInfo, hitRange, playerMask))
            {
                if (hitInfo.transform.tag == "CanPickup")
                {
                    //If true, freeze objects physics & move it to the player's "hand" (Hold Point)
                    hitInfo.transform.GetComponent<Rigidbody>().isKinematic = true;
                    hitInfo.transform.SetParent(holdPoint);
                    hitInfo.transform.localPosition = Vector3.zero;
                    hitInfo.transform.localRotation = Quaternion.identity;
                }
            }
        }
    }

    /// <summary>
    /// Handles the ability to throw held objects
    /// </summary>
    void HandleThrowing()
    {
        // Only continue if the player holding something
        if (holdPoint.childCount == 0)
        {
            return;
        }

        // If input, release held object and add a set force to it
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            powerbarSlider.value = 0;
            sliderFillImage.color = Color.red;
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            powerbarSlider.value += fillRate * Time.deltaTime;
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            // (Release)
            GameObject thrownItem = holdPoint.GetChild(0).gameObject;
            thrownItem.transform.parent = null;
            thrownItem.transform.position = firePoint.position;

            // (Throw)
            Rigidbody rb = thrownItem.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(playerCamera.forward * maxThrowForce * powerbarSlider.value, ForceMode.Force);
            powerbarSlider.value = 1;
            sliderFillImage.color = Color.white;
        }
    }
}
