using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{

    public GameObject Player;
    public GameObject MainCamera;
    public GameObject Noise;
    public GameObject Target;
    public bool DoorCheck;


    // Start is called before the first frame update
    public virtual void Start()
    {
        Player = GameObject.Find("PrototypeHero");
        MainCamera = GameObject.Find("CRTCamera");
        Noise = GameObject.Find("Canvas");
        DoorCheck = false;
        FindOutDoor();
    }
    
    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.UpArrow) && DoorCheck == true)
        {
            Debug.Log("checked2");
            DoorTeleport(Target);
          
        }
        */

    }
    /*
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("checked1");
         
        if (collision.CompareTag("Player"))
        {
            DoorCheck = true;
            Debug.Log("checked2");
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
    */

    public void DoorTeleport(GameObject Target)
    {
        Vector3 Ptemp = Player.transform.position;
        Vector3 Ctemp = MainCamera.transform.position;
        Vector3 Outvec3 = Target.transform.position;
        Ptemp.x = Outvec3.x - 0.5f;
        Ctemp.x = Outvec3.x - 0.5f;
        Player.transform.position = Ptemp;
        MainCamera.transform.position = Ctemp;
        Noise.GetComponent<NoiseEffect>().StartNose(0.1f);
        MainCamera.GetComponent<FollowCam>().ShakeTime = 0.5f;
    }

    public void FindOutDoor()
    {
        Target = transform.GetChild(0).gameObject;
    }


}
