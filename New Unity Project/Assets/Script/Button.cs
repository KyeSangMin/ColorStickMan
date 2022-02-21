using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator anima;
    public float Time;

    private bool ButtonState;

    void Start()
    {
        anima = GetComponent<Animator>();
        ButtonState = false;
    }

    // Update is called once per frame
    void Update()
    {


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
        else
        {
            //ImageSwitcher.instance.SetColorReset();
            GameObject.Find("Switcher").GetComponent<ImageSwitcher>().SetColorReset();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anima.SetBool("Button", true);
        ButtonState = true;
        
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

    }

}
