using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{


    public int Event;

    GameObject gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("EventSystem");
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gamemanager.GetComponent<GameManager>().SetEventNumber(Event);
            gamemanager.GetComponent<GameManager>().SetNumToString();
        }
    }
}
