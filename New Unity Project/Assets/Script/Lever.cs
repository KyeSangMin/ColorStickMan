using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Animator animator;

    private GameObject actBlock;
    private bool OnLever = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("OnLever", false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("OnLever") && OnLever && Input.GetKeyDown(KeyCode.F))
            Push_Lever();
        
        else if (animator.GetBool("OnLever") && OnLever && Input.GetKeyDown(KeyCode.F))
            Pull_Lever();
            
    }

    void Push_Lever()
    {
        animator.SetBool("OnLever", true);
        actBlock.SetActive(true);
        
    }

    void Pull_Lever()
    {
        animator.SetBool("OnLever", false);
        actBlock.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "PrototypeHero")
            OnLever = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name=="PrototypeHero")
            OnLever = false;
    }

}
