using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseEffect : MonoBehaviour
{

    public float NoiseTime;
    public GameObject NoseImage;
    GameObject Hero;

    // Start is called before the first frame update
    void Start()
    {


        //NoseImage = GameObject.Find("Noise");
        Hero = GameObject.Find("PrototypeHero");
    }

    // Update is called once per frame
    void Update()
    {

        if(Hero.GetComponent<PrototypeHero>().GetDead() == true)
        {
            StartNose(0.1f);
        }
        
        

    }



    public void StartNose(float NoiseTime)
    {
        NoseImage.SetActive(true);
        StartCoroutine(NoiseDelay(NoiseTime));
    }
    IEnumerator NoiseDelay(float NoiseTime)
    {

        yield return new WaitForSeconds(NoiseTime);
        NoseImage.SetActive(false);

    }







}
