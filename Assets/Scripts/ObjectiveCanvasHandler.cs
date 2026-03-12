using TMPro;
using UnityEngine;

public class ObjectiveCanvasHandler : MonoBehaviour
{
    TextMeshProUGUI currentChapter;
    TextMeshProUGUI currentObjective;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentChapter = GameObject.FindGameObjectWithTag("ChapterText").GetComponent<TextMeshProUGUI>() ;
        currentObjective = GameObject.FindGameObjectWithTag("ObjectiveText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        currentChapter.text = GameManager.instance.currentIndex.SequenceName;
        currentObjective.text = GameManager.instance.currentIndex.objectiveText;

    }
}
