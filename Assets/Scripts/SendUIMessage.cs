using TMPro;
using UnityEngine;

public class SendUIMessage : MonoBehaviour
{
    public enum UIType
    {
        ObjectiveUIText,
        ActionUIText
    }

    [SerializeField]
    TextMeshProUGUI ActionUI;
    [SerializeField]
    string UIText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActionUI.text = UIText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        ActionUI.text = UIText;
    }

    void OnTriggerEnter(Collider other)
    {
        ActionUI.gameObject.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        ActionUI.gameObject.SetActive(false);
    }
}
