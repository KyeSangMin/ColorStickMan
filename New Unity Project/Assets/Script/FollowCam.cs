using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public Transform Player;
    public float movetime=0.01f;
    Vector3 camPos;

    //카메라 흔들기
    //참고 코드 출처: https://chameleonstudio.tistory.com/55

    public float ShakeAmount;
    //흔들리는 강도

    public float ShakeTime;
    //흔들리는 시간 0이상으로 설정시 바로 Shake 실행

    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {

        camPos = new Vector3(Player.position.x, Player.position.y + 1, -10);

        transform.position = Vector3.MoveTowards(gameObject.transform.position, camPos, movetime);

        ShakeCam();

    }

    void ShakeCam()
    {
        if (ShakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * ShakeAmount + transform.position;

            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0.0f;
        }

    }
}
