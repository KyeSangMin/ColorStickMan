using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator anima;
    public float Time;

    private bool ButtonState; //넣은 이유를 못찾을시 삭제

    public GameObject[] RedButtontag; 

    void Start()
    {
        anima = GetComponent<Animator>();
        ButtonState = false;
        RedButtontag = GameObject.FindGameObjectsWithTag("RedButton");
       
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


    

    IEnumerator ButtonDelay(float Time)
    {

        yield return new WaitForSeconds(Time);

        ButtonState = false;
        anima.SetBool("Button", false);
        if (ButtonState == false)
        {
            //ImageSwitcher.instance.SetColorReset();
            GameObject.Find("Switcher").GetComponent<ImageSwitcher>().SetColorReset();
            Debug.Log("reset");
        }

    }

}
