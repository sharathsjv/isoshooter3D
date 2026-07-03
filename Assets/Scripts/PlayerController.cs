using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using UnityEngine.TextCore.Text;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Rig rigLayer_Shooting;
    
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
    [SerializeField]
    GameObject AimCylinder;

    public InputAction movementAction;
    public PlayerInput playerInput;

    public bool isDiving,isUp = true;
    public Vector3 diveDirection;
    [SerializeField]
    float diveSpeed = 10;
    [SerializeField]
    RecoilHandler LookAtIk;

    [SerializeField]
    public bool interactInput;

    [SerializeField]
    public float UpDownInput;
    public float MaxYVertical, MinYVertical;
    

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        if (characterController == null)
        {
            Debug.LogError("Player needs a CharacterController component!");
        }

        playerInput = GetComponent<PlayerInput>();
        movementAction = playerInput.actions.FindAction("Movement");
        LookAtIk = GetComponentInChildren<RecoilHandler>();
    }
    void Update()
    {
        // transform.position.y=0f;
        animator.SetBool("moveInput",moveInput);
        animator.SetBool("isUp", isUp);
        rigLayer_Shooting.weight = aimVector.magnitude;
        StrafeAngle = Vector3.SignedAngle(aimVector, moveVector, transform.up);
        animator.SetFloat("StrafeAngle", StrafeAngle);
        
        if (isUp)
            HandleMovement();
        
        
        if (aimInput&&moveInput&&isUp)
        {
            //rigLayer_Shooting.weight = aimVector.magnitude;
            HandleRotation(aimHorizontal,aimVertical);
            
            //if (characterController.attachedRigidbody.linearVelocity>)
            
        }
        else if (!aimInput&&moveInput&&isUp)
        {
            HandleRotation(horizontalInput,verticalInput);
        }
        else if (aimInput&&!moveInput&&isUp)
        {
            HandleRotation(aimHorizontal,aimVertical);
        }
        else if (aimInput&&isDiving)
        {
            HandleRotation(aimHorizontal,aimVertical);
        }

        if (isDiving)
        {
            characterController.Move(diveDirection*diveSpeed*Time.deltaTime);
            //LookAtIk.transform.position = new Vector3(10*Mathf.Cos(StrafeAngle),LookAtIk.transform.position.y,10*Mathf.Sin(StrafeAngle));
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
            animator.SetTrigger("Strafe");
            AimCylinder.SetActive(true);
            if (moveInput)
            {   
                
            }
        }
        if (context.canceled)
        {
            aimInput =false;
            animator.SetTrigger("NotStrafe");
            AimCylinder.SetActive(false);

        }
        
    }

    public void Dive(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("Dive");
            diveDirection = moveDirection;
        }

        if (context.canceled)
        {
        }
    }

    public void InteractInput(InputAction.CallbackContext context)
    {
        // interactInput = context.started;
        // if (interactInput)
        // {
        //     Debug.Log("Yes");
        // }

        if (context.started)
        {
            interactInput = true;
        }
        if (context.canceled)
        {
            interactInput = false;
        }
    }

    public void SetIsUp(bool thevalue)
    {
        isUp = thevalue;
    }

    public void MovePlayerStatic(Transform newposition)
    {
        transform.position = newposition.position;
    }

    public void LookUpDown(InputAction.CallbackContext context)
    {
        if (aimInput)
        {
            if (context.started)
            {
                UpDownInput = context.ReadValue<float>();

                if (UpDownInput!=0)
                {
                    if (LookAtIk.transform.position.y<MaxYVertical||LookAtIk.transform.position.y>MinYVertical)
                    {
                        LookAtIk.currentTransform += new UnityEngine.Vector3(0f,UpDownInput,0f);
                    }
                }
            }
        }
    }

    
}
