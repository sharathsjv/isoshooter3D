using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using Unity.Cinemachine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField]
    public Collider playerCollider;
    [SerializeField]
    Vector3 moveinput;
    [SerializeField]
    Quaternion toTurnAngle;
    [SerializeField]
    public bool isCrouching, isStrafing;
    public Animator anim;
    [SerializeField]
    float walkSpeed;
    [SerializeField]
    int crouchSpeed;
    [SerializeField]
    Transform PlayerAimTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        playerCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        
        //toTurnAngle = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + Mathf.Atan2(moveinput.x, moveinput.z) * Mathf.Rad2Deg, 0);
        if (moveinput != Vector3.zero && !isStrafing)
        {
            toTurnAngle = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + Mathf.Atan2(moveinput.x, moveinput.z) * Mathf.Rad2Deg, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, toTurnAngle, 10 * Time.fixedDeltaTime);
            rb.transform.Translate(0, 0, moveinput.magnitude * Time.fixedDeltaTime * walkSpeed);
        }
        else if (moveinput != Vector3.zero && isStrafing)
        {
            rb.transform.Translate(moveinput.x*walkSpeed*Time.deltaTime, 0, moveinput.z * Time.fixedDeltaTime * walkSpeed);
        }
        


    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        moveinput = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        anim.SetFloat("Input Y", moveinput.z);
        anim.SetFloat("Input X", moveinput.x);
        //toTurnAngle = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + Mathf.Atan2(moveinput.x, moveinput.z) * Mathf.Rad2Deg, 0);
        if (context.started)
        {
            
        }

        if (context.canceled)
        {
            anim.SetFloat("Input Y", 0);
            anim.SetFloat("Input X", 0);
        }   
    }

    public void Strafe (InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isStrafing = true;
            anim.SetTrigger("Strafe"); 
        }
        if (context.canceled)
        {
            isStrafing = false;
            anim.SetTrigger("NotStrafe");
        }
    }

    public void LookorAim(InputAction.CallbackContext context)
    {
        if (context.started)
            PlayerAimTransform.position = new Vector3(context.ReadValue<Vector2>().x,0,context.ReadValue<Vector2>().y);
        if (context.canceled)
        {
            PlayerAimTransform.position = Vector3.zero;
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
