using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//참고 코드 출처: https://chameleonstudio.tistory.com/55
public class Cameraeffect : MonoBehaviour{


    public float ShakeAmount;

    public float ShakeTime;
    Vector3 initialPos;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        initialPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 5f);

        if (ShakeTime>0)
        {
            transform.position = Random.insideUnitSphere * ShakeAmount+initialPos;

            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0.0f;
            transform.position = initialPos;
        }
        
    }
}
