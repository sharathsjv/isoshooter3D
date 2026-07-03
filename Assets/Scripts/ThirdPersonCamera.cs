using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;

    public float sensitivity = 120f;

    public float distance = 4f;

    public float height = 1.7f;

    float yaw;
    float pitch = 20f;

    Vector2 lookInput;

    void LateUpdate()
    {
        yaw += lookInput.x * sensitivity * Time.deltaTime;
        pitch -= lookInput.y * sensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, -40f, 75f);

        Quaternion rotation =
            Quaternion.Euler(pitch, yaw, 0);

        Vector3 position =
            target.position
            - rotation * Vector3.forward * distance
            + Vector3.up * height;

        transform.position = position;
        transform.rotation = rotation;
    }

    public void LookInput(InputAction.CallbackContext ctx)
    {
        lookInput = ctx.ReadValue<Vector2>();
    }
}