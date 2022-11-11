using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISensor : MonoBehaviour
{
    GameObject UIImage;
    GameObject InputCon;

    bool StartActiveDialogue;
    bool NextActiveDialogue;

    // Start is called before the first frame update
    void Start()
    {
        UIImage = transform.GetChild(0).gameObject;
        UIImage.SetActive(false);
        InputCon = GameObject.Find("InputSystem");
        StartActiveDialogue = false;
        NextActiveDialogue = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (InputCon.GetComponent<InputControl>().InputCon == false)
        {

            if (StartActiveDialogue == true && Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("PressG");
                FindObjectOfType<DialogueTrigger>().TriggerDialogue();
                NextActiveDialogue = true;
                StartActiveDialogue = false;

            }

            else if(NextActiveDialogue == true && Input.GetKeyDown(KeyCode.G))
            {
                FindObjectOfType<DialogueSystem>().DisPlayNextSentence();
            }


        }

        else if (InputCon.GetComponent<InputControl>().InputCon == true)
        {

        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            UIImage.SetActive(true);
            StartActiveDialogue = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            UIImage.SetActive(false);
            StartActiveDialogue = false;
        }
    }


    public void ReSetDialogue()
    {
        NextActiveDialogue = false;
        StartActiveDialogue = true;

    }


}
