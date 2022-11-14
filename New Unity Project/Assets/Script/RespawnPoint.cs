using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject Player;

    void Start()
    {
        Player = GameObject.Find("PrototypeHero");
    }

    // Update is called once per frame
   

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.GetComponent<PrototypeHero>().SetRespownPoint(this.gameObject);
        }



    }







}
