using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Animator animator;

    private GameObject actBlock;
    private GameObject[] ChildBlock;
    private bool OnLever = false;
    private bool CheckThrow;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("OnLever", false);


        ChildBlock = new GameObject[this.transform.childCount];

        for (int i = 0; i < this.transform.childCount; i++)
        {
            ChildBlock[i] = this.transform.GetChild(i).gameObject;

        }

        for (int i = 0; i < this.transform.childCount; i++)
        {
            if(ChildBlock[i].CompareTag("OnLever"))
            {
                ChildBlock[i].SetActive(true);
                animator.SetBool("OnLever", true);
            }
            else
            {
                ChildBlock[i].SetActive(false);
            }
            
        }
        CheckThrow = GameObject.Find("PrototypeHero").GetComponent<PrototypeHero>().m_Throwing;

        

    }

    // Update is called once per frame
    void Update()
    {
        CheckThrow = GameObject.Find("PrototypeHero").GetComponent<PrototypeHero>().m_Throwing;



        if (Input.GetKeyDown(KeyCode.F) && !CheckThrow)
        {
            if (!animator.GetBool("OnLever") && OnLever)
                Push_Lever();

            else if (animator.GetBool("OnLever") && OnLever)
                Pull_Lever();
        }  
    }

    void Push_Lever()
    {
        animator.SetBool("OnLever", true);
        GameObject.Find("CRTCamera").GetComponent<FollowCam>().ShakeTime = 0.5f;
        //actBlock.SetActive(true);

        for (int i = 0; i < this.transform.childCount; i++)
        {
            if(ChildBlock[i].activeSelf == false)
            {
                ChildBlock[i].SetActive(true);
            }
            else
            {
                ChildBlock[i].SetActive(false);
            }
        }

        
    }

    void Pull_Lever()
    {
        animator.SetBool("OnLever", false);
        GameObject.Find("CRTCamera").GetComponent<FollowCam>().ShakeTime = 0.5f;
        //actBlock.SetActive(false);
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "PrototypeHero")
            OnLever = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name == "PrototypeHero")
            OnLever = false;
    }

}
