using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDoor : DoorSystem // DoorSystem �ڷ���Ʈ ��� ���� 
{
  
    GameObject ColorSwich;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ColorSwich = GameObject.Find("Switcher");
    }

    // Update is called once per frame
    void Update() 
    {

        if (Input.GetKeyDown(KeyCode.UpArrow) && DoorCheck == true)
        {
            Debug.Log("checked2");
            DoorTeleport(Target);
            SwitchColor();

        }



    }


    void SwitchColor()          // Door �̵��� �� ��ȯ
    {
        /*
        if(this.CompareTag("RedDoor"))
        {
            ColorSwich.GetComponent<ImageSwitcher>().SetColorRed();
        }
        */

        switch (this.tag)
        {
            case ("RedDoor"):
                ColorSwich.GetComponent<ImageSwitcher>().SetColorRed();
                break;

            case ("BlueDoor"):
                ColorSwich.GetComponent<ImageSwitcher>().SetColorBlue();
                break;

            case ("GreenDoor"):
                ColorSwich.GetComponent<ImageSwitcher>().SetColorGreen();
                break;

            default:
                ColorSwich.GetComponent<ImageSwitcher>().SetColorReset();
                break;

        }

        
    }


    void ReSetColor()
    {
        ColorSwich.GetComponent<ImageSwitcher>().SetColorReset();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("checked1");

        if (collision.CompareTag("Player"))
        {
            DoorCheck = true;
            //Debug.Log("checked2");
        }

        else
        {
            return;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DoorCheck = false;
        }

        else
        {
            return;
        }
    }
}
