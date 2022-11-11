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

    Coroutine ButtonRoutine;




    void Start()
    {
        anima = GetComponent<Animator>();
        ButtonState = false;


        ParentButton = GameObject.Find("Switch");
        ChildArray = new GameObject[ParentButton.transform.childCount];
       for(int i = 0; i < ParentButton.transform.childCount;i++)
        {
            ChildArray[i] = ParentButton.transform.GetChild(i).gameObject;
            
        }

        ButtonRoutine = StartCoroutine(ButtonDelay(Time));
    }

    // Update is called once per frame
    void Update()
    {


       
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if(!ButtonState)
        {
            anima.SetBool("Button", true);
            ButtonState = true;
        
            GameObject.Find("CRTCamera").GetComponent<FollowCam>().ShakeTime = 0.5f;

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

                GameObject.Find("ButtonSystem").GetComponent<ButtonSystem>().PushIndex(this.gameObject);

            }
        }
        else
        {
            
            anima.SetBool("Button", true);
            ButtonState = true;
            
        }

      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       

        if (ButtonState)
        {
            StartCoroutine(ButtonDelay(Time));
        }

    }

    public bool GetButtonState()
    {
        return ButtonState;
    }

    public void ResetIEnum()
    {

        ButtonState = false;
        anima.SetBool("Button", false);
        
    }


    public bool CheckState()
    {

        for (int i = 0; i < ChildArray.Length; i++)
        {
           
            ButtonComparison = ChildArray[i].GetComponent<Button>().GetButtonState();

            if (ButtonComparison == true && ChildArray[i].gameObject != this.gameObject)
            {
                // ButtonState = true;
                //ChildArray[i].GetComponent<Button>().ResetIEnum();
                // Debug.Log("stop" + gameObject.name);
                return true;
            }
        }


        return false;
    }

    public void SetButtonState()
    {

        for (int i = 0; i < ChildArray.Length; i++)
        {
       
            ButtonComparison = ChildArray[i].GetComponent<Button>().GetButtonState();

            if (ButtonComparison == true && ChildArray[i].gameObject != this.gameObject)
            {

                ChildArray[i].GetComponent<Button>().ButtonState = false;
               
                
            }
        }
    }

    public void ResetState()
    {

        for (int i = 0; i < ChildArray.Length; i++)
        {

            ButtonComparison = ChildArray[i].GetComponent<Button>().ButtonState = false;

       
        }
    }

    public void StopRoution()
    {
        StopCoroutine(ButtonRoutine);
    }

    IEnumerator ButtonDelay(float Time)
    {

        yield return new WaitForSeconds(Time);

        ButtonState = false;
        /*
        for (int i = 0; i < ChildArray.Length; i++)
        {
            Debug.Log("check1");
            ButtonComparison = ChildArray[i].GetComponent<Button>().GetButtonState();

            if (ButtonComparison == true && ChildArray[i].gameObject != this.gameObject)
            {
                ButtonState = true;
                Debug.Log("check2");
               // ChildArray[i].GetComponent<Button>().StopRoution();
                ChildArray[i].GetComponent<Button>().ResetIEnum();
                Debug.Log("stop"+gameObject.name);
            }
        }

        */
       

        if (!ButtonState && !CheckState())
        {
            ResetState();
            ResetIEnum();
            
            anima.SetBool("Button", false);
           

            if (GameObject.Find("ButtonSystem").GetComponent<ButtonSystem>().CheckIndex(this.gameObject))
            {
                GameObject.Find("Switcher").GetComponent<ImageSwitcher>().SetColorReset();
                GameObject.Find("ButtonSystem").GetComponent<ButtonSystem>().ResetIndex();
                GameObject.Find("CRTCamera").GetComponent<FollowCam>().ShakeTime = 0.5f;
            }
        }

        else
        {
            SetButtonState();
            ResetIEnum();
            anima.SetBool("Button", false);
            
        }
       
   
 
        
    }

}
