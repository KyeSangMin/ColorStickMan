using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator anima;
    public float Time;

    private bool ButtonState; //넣은 이유를 못찾을시 삭제

    public GameObject[] ChildArray;
    public GameObject ParentButton;

    private bool ButtonComparison;



    void Start()
    {
        anima = GetComponent<Animator>();
        ButtonState = false;

        ParentButton = GameObject.Find("Switch");
        ChildArray = new GameObject[ParentButton.transform.childCount];
       for(int i = 0; i<ParentButton.transform.childCount;i++)
        {
            ChildArray[i] = ParentButton.transform.GetChild(i).gameObject;
            Debug.Log(i);
        }
    }

    // Update is called once per frame
    void Update()
    {


       
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anima.SetBool("Button", true);
        ButtonState = true;

        if (anima.GetBool("Button") && ButtonState == true)
        {
            GameObject.Find("Switcher").GetComponent<ImageSwitcher>().SetColorReset();
            //ImageSwitcher.instance.SetColorRed();
            if (gameObject.CompareTag("RedButton"))
            {
                GameObject.Find("Switcher").GetComponent<ImageSwitcher>().SetColorRed();
            }
            else if (gameObject.CompareTag("BlueButton"))
            {
                GameObject.Find("Switcher").GetComponent<ImageSwitcher>().SetColorBlue();
            }
            else if (gameObject.CompareTag("GreenButton"))
            {
                GameObject.Find("Switcher").GetComponent<ImageSwitcher>().SetColorGreen();
            }

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*
        if (ButtonState == true)
            return;
        else
        {
            anima.SetBool("Button", false);
        }
        */
        StartCoroutine(ButtonDelay(Time));
    }

    public bool GetButtonState()
    {
        return ButtonState;
    }

    

    IEnumerator ButtonDelay(float Time)
    {

        yield return new WaitForSeconds(Time);

        
        anima.SetBool("Button", false);

        for (int i = 0; i < ChildArray.Length; i++)
        {
            Debug.Log("check1");
            ButtonComparison = ChildArray[i].GetComponent<Button>().GetButtonState();
            if (ButtonComparison == true)
            {
                ButtonState = true;
                Debug.Log("check2");
            }
        }

        if (ButtonState == false)
        {
            //ImageSwitcher.instance.SetColorReset();
            GameObject.Find("Switcher").GetComponent<ImageSwitcher>().SetColorReset();
            
        }

        ButtonState = false;

    }

}
