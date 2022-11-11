using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    // Start is called before the first frame update

    public bool InputCon;
    public bool DialouguePrint;


    private bool InputSwich;


    void Start()
    {
        InputCon = false;
        InputSwich = false;
        DialouguePrint = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            InputStop();
        }



    }


    void InputStop()
    {
        if (InputSwich == false)
        {
            InputCon = true;
            InputSwich = true;
            Debug.Log("∏ÿ√„");
        }
        else if (InputSwich == true)
        {
            InputCon = false;
            InputSwich = false;
            Debug.Log("¿€µø");
        }
    }
}
