using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{

    public Animator anima;
    private GameObject[] ChildBlock;



    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animator>();

        ChildBlock = new GameObject[this.transform.childCount];

        for (int i = 0; i < this.transform.childCount; i++)
        {
            ChildBlock[i] = this.transform.GetChild(i).gameObject;

        }

        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (ChildBlock[i].CompareTag("OnLever"))
            {
                ChildBlock[i].SetActive(true);
                anima.SetBool("OnLever", true);
            }
            else
            {
                ChildBlock[i].SetActive(false);
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        anima.SetBool("Button", true);
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (ChildBlock[i].activeSelf == false)
            {
                ChildBlock[i].SetActive(true);
            }
            else
            {
                ChildBlock[i].SetActive(false);
            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        anima.SetBool("Button", false);
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (ChildBlock[i].activeSelf == true)
            {
                ChildBlock[i].SetActive(false);
            }
            else
            {
                ChildBlock[i].SetActive(true);
            }
        }
    }

}
