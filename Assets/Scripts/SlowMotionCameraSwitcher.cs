using UnityEngine;
using System.Collections;
using Unity.Cinemachine; // Use for Cinemachine 3.x (Unity 2023+)

public class SlowMotionCameraSwitcher : MonoBehaviour
{
    
    [SerializeField] private CinemachineCamera timedCamera;
    [SerializeField] private CinemachineCamera currentCamera;


    [SerializeField] private float durationInSeconds = 3.0f; 
    [SerializeField] private float slowMoScale = 0.2f; // 0.2 means 20% normal speed

    public void TriggerSlowMoCamera()
    {
        StartCoroutine(SlowMoCameraRoutine());
    }

    private IEnumerator SlowMoCameraRoutine()
    {
        // 1. Activate camera and slow down time
        currentCamera.gameObject.SetActive(false);
        timedCamera.gameObject.SetActive(true);
        timedCamera.Priority = 20; 
        Time.timeScale = slowMoScale;
        
        // Adjust fixedDeltaTime so physics and animations remain smooth in slow-mo
        Time.fixedDeltaTime = 0.02f * Time.timeScale; 

        // 2. Wait using REALTIME seconds (unaffected by slow-mo)
        yield return new WaitForSecondsRealtime(durationInSeconds);

        // 3. Reset camera and restore normal time speed
        currentCamera.gameObject.SetActive(true);
        timedCamera.Priority = 0; 
        timedCamera.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f; 
    }
}
