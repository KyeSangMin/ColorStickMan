using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public string EventString;

    private Vector3 ResPos;


    private GameObject PlayerData;
    private GameObject DialogueEventData;










    // Start is called before the first frame update
    void Start()
    {
        PlayerData = GameObject.Find("PrototypeHero");
        EventString = "1";



    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public string CheckEvent()
    {
        return EventString;
    }

    public void SetEventString(string eventnum)
    {

        EventString = eventnum;

    }

    public void GetResPos()
    {
        ResPos = PlayerData.transform.position;

    }

    


}
