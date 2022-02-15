using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator anima;

    void Start()
    {
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

       // anima.SetBool("Button",true);

        



        if (anima.GetBool("Button"))
        {
            ImageSwitcher.instance.SetColorRed();
        }
        else
        {
            ImageSwitcher.instance.SetColorReset();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anima.SetBool("Button", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anima.SetBool("Button", false);
    }



}
