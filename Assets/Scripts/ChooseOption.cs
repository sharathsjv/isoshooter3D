using UnityEngine;
using UnityEngine.Events;

public class ChooseOption : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnStartFunctions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnStartFunctions.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
