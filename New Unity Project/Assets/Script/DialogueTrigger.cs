using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public Dialogue dialogue;


    private void Awake()
    {
        FindObjectOfType<DialogueSystem>().SplitDialogue(dialogue);
    }

    public void TriggerDialogue()
    {
        
        FindObjectOfType<DialogueSystem>().StartDialogue(dialogue);
    }


    


}
