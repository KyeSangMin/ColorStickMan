using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSystem : MonoBehaviour
{

    GameObject Player;
    GameObject Camera;
    GameObject Head;
    public GameObject Tail;

    bool Loop;
    // Start is called before the first frame update
    void Start()
    {

        Player = GameObject.Find("PrototypeHero");
        Camera = GameObject.Find("CRTCamera");
        
        Loop = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && Loop == false)
        {
            Teleport(Tail);
        }

        else
        {
            return;
        }
    }




    public void Teleport(GameObject Target)
    {
        Vector3 Ptemp = Player.transform.position;
        Vector3 Ctemp = Camera.transform.position;
        Vector3 Tailvec3 = Target.transform.position;
        Ptemp.x = Tailvec3.x - 0.5f;
        Ctemp.x = Tailvec3.x - 0.5f;
        Player.transform.position = Ptemp;
        Camera.transform.position = Ctemp;
        
    }
}
