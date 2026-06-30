using UnityEngine;

public class TestFall : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.CrossFade("Falling", 0.1f);
        }
    }
}