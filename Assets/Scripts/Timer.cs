using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnCompleteFunction, OnStartFunctions;
    [SerializeField]
    float Time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnStartFunctions.Invoke();
        StartCoroutine(CountDownTimer(Time));
        
        
    }

    void OnEnable()
    {
        OnStartFunctions.Invoke();
        StartCoroutine(CountDownTimer(Time));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CountDownTimer(float duration)
    {
        Debug.Log("start");
        
        yield return new WaitForSeconds(duration);
        OnCompleteFunction.Invoke();
        
        Debug.Log("Stop");
        
    }
}
