using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public float SequenceID;
    public List<DialogueObject> DialogueSequence;
    [SerializeField]
    TextMeshProUGUI dialogueTextBox, nameText;
    [SerializeField]
    DialogueObject currentDialogueObject;
    [SerializeField]
    int Dialoguecounter, DialogueSequenceCounter;
    [SerializeField]
    PlayerInput MainPlayerInput, DialoguePlayerInput;
    [SerializeField]
    UnityEvent OnCompleteFunctions;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentDialogueObject = DialogueSequence[0];
        DialogueSequenceCounter = 0;
        Dialoguecounter = 0;
        RefreshAndDisplayDialogue(Dialoguecounter,true);
    }

    void OnEnable()
    {
        if (DialoguePlayerInput==null)
        {
            DialoguePlayerInput = GetComponent<PlayerInput>();
        }
        MainPlayerInput.enabled = false;
        DialoguePlayerInput.enabled = true;
        
    }

    public void NextDialogue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Dialoguecounter<currentDialogueObject.Dialogues.Count-1)
            {
                Dialoguecounter++;
                RefreshAndDisplayDialogue(Dialoguecounter);
            
            }
            else //if (currentDialogueObject.Dialogues[Dialoguecounter+1]==null)                        
            {
                if (DialogueSequenceCounter<DialogueSequence.Count-1)
                {
                    DialogueSequenceCounter++;
                    currentDialogueObject = DialogueSequence[DialogueSequenceCounter];
                    Dialoguecounter = 0;
                    RefreshAndDisplayDialogue(Dialoguecounter, true);
                }
                else                                                                                        //This is where the sequence ends
                {
                    DialoguePlayerInput.enabled = false;
                    MainPlayerInput.enabled = true;
                    Debug.Log("Dialogue Sequence Over");
                    OnCompleteFunctions.Invoke();
                    this.gameObject.SetActive(false);
                    
                }
            }
            
        }
    }

    void RefreshAndDisplayDialogue(int DialogueCounter)
    {
        dialogueTextBox.text = currentDialogueObject.Dialogues[DialogueCounter];

    }

    void RefreshAndDisplayDialogue(int DialogueCounter, bool updateName)
    {
        dialogueTextBox.text = currentDialogueObject.Dialogues[DialogueCounter];
        nameText.text = currentDialogueObject.CharacterName;
    }
}
