using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public Transform Player;
    public float movetime=0.01f;
    Vector3 camPos;



    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {

        camPos = new Vector3(Player.position.x, Player.position.y + 1, -10);

        transform.position = Vector3.MoveTowards(gameObject.transform.position, camPos, movetime);

    }
}
