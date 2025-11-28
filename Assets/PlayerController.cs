using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float StrafeAngle;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f; // For smooth rotation, if desired
    public float horizontalInput, verticalInput, aimHorizontal, aimVertical;
    private CharacterController characterController;
    [SerializeField]
    private UnityEngine.Vector3 moveDirection = UnityEngine.Vector3.zero;
    [SerializeField]
    private UnityEngine.Vector3 lookDirection = UnityEngine.Vector3.zero;
    public Animator animator;
    public bool moveInput, aimInput;
    [SerializeField]
    Vector3 moveVector, aimVector, crossProduct;
    

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        if (characterController == null)
        {
            Debug.LogError("Player needs a CharacterController component!");
        }
    }
    void Update()
    {
        StrafeAngle = Vector3.SignedAngle(aimVector, moveVector, transform.up);
        HandleMovement();
        if (aimInput&&moveInput)
        {
            HandleRotation(aimHorizontal,aimVertical);
            animator.SetFloat("StrafeAngle", StrafeAngle);
            //if (characterController.attachedRigidbody.linearVelocity>)
            
        }
        else
        {
            HandleRotation(horizontalInput,verticalInput);
        }
    }

    private void HandleMovement()
    {
        // Get input for movement (left stick or WASD)

        // Calculate movement relative to the camera's forward and right directions
        UnityEngine.Vector3 cameraForward = Camera.main.transform.forward;
        UnityEngine.Vector3 cameraRight = Camera.main.transform.right;

        // Flatten the vectors to the XZ plane for isometric movement (no vertical movement on Y axis)
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();
        moveDirection = cameraRight * horizontalInput + cameraForward * verticalInput;
        animator.SetFloat("Input X", horizontalInput);
        animator.SetFloat("Input Y", verticalInput);

        
        // Use the CharacterController to move the player
        // Apply gravity separately if needed, or use a simple move logic
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleRotation(float genericaimhorizontal, float genericaimvertical)
    {
        // Get input for aiming (right stick or mouse input, you need to configure "FireHorizontal"/"FireVertical" axes)

        // Check if there is significant aiming input
        if (Mathf.Abs(genericaimhorizontal) > 0.1f || Mathf.Abs(genericaimvertical) > 0.1f)
        {
            // Create a look direction vector (relative to the world in XZ plane)
            lookDirection = new Vector3(genericaimhorizontal, 0f, genericaimvertical).normalized;
            if (aimInput)
            {    
                
            }
            // Rotate the player to look in the aim direction
            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                // Use Quaternion.Slerp for smooth rotation (optional)
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                // Or snap rotation immediately:
                // transform.rotation = targetRotation; 
            }
        }
        
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        moveVector = new Vector3(context.ReadValue<Vector2>().x,0,context.ReadValue<Vector2>().y);
        horizontalInput = context.ReadValue<Vector2>().x;
        verticalInput = context.ReadValue<Vector2>().y;
        if (context.started)
        {
            moveInput = true;
        }
        if (context.canceled)
        {
            moveInput = false;
        }

        
        
    }

    public void AimInput(InputAction.CallbackContext context)
    {
        aimVector = new Vector3(context.ReadValue<Vector2>().x,0,context.ReadValue<Vector2>().y);
        aimHorizontal = context.ReadValue<Vector2>().x;
        aimVertical = context.ReadValue<Vector2>().y;
        if (context.started)
        {
            aimInput = true;
            if (moveInput)
            {   
                animator.SetTrigger("Strafe");
                
            }
        }
        if (context.canceled)
        {
            aimInput =false;
            animator.SetTrigger("NotStrafe");

        }
        
    }
}
