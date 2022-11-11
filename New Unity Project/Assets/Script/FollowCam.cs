using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public Transform Player;
    public Transform ZoomPos;
    public GameObject ZoomObject;
    public float movetime=0.01f;
    Vector3 camPos;
    Vector3 ZoomP;
    public bool Tracking;
    public bool Zoom;
    public bool checkstate;

    public float smoothTime = 0.1f;
    private Camera cam;
    private float targetZoomSize = 10f;
    private const float roundReadyZoomSize = 14.5f;
    private const float readyShotZoomSize = 5f;
    private const float trackingZoomSize = 10f;
    private float resettargetZoomSize = 4f;

    private float lastZoomSpeed;

    GameObject InputCon;

    //ī�޶� ����
    //���� �ڵ� ��ó: https://chameleonstudio.tistory.com/55

    public float ShakeAmount;
    //��鸮�� ����

    public float ShakeTime;
    //��鸮�� �ð� 0�̻����� ������ �ٷ� Shake ����

    // Start is called before the first frame update
    void Start()
    {
        Tracking = true;
        Zoom = false;
        checkstate = false;

        cam = this.GetComponent<Camera>();
        InputCon = GameObject.Find("InputSystem");
    }

    // Update is called once per frame
    void Update()
    {

        if(InputCon.GetComponent<InputControl>().InputCon == false)
        {

        if (Input.GetKeyDown("z") && !checkstate)
        {
            Tracking = false; 
            Zoom = true;
            checkstate = true;
        }
        else if (Input.GetKeyDown("z") && checkstate)
        {
            
            Tracking = true;
            Zoom = false;
            checkstate = false;
        }


        if (Tracking && !Zoom)
        {
            ResetZoom();
            camPos = new Vector3(Player.position.x, Player.position.y + 1, -10);
            ZoomP = new Vector3(Player.position.x, 6.44f, 0);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, camPos, movetime);
            ZoomObject.transform.position = Vector3.MoveTowards(ZoomObject.transform.position, ZoomP, movetime);
        }
        else if (Zoom && !Tracking)
        {

            Zoomcam();
        }

        ShakeCam();

        }

        else if(InputCon.GetComponent<InputControl>().InputCon == true)
        {

        }


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

    void Zoomcam()
    {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoomSize,
                                       ref lastZoomSpeed, smoothTime);

        cam.orthographicSize = smoothZoomSize;

        camPos = new Vector3(ZoomPos.position.x, ZoomPos.position.y , -10);

        transform.position = Vector3.MoveTowards(gameObject.transform.position, camPos, movetime);
    }

    void ResetZoom()
    {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, resettargetZoomSize,
                                      ref lastZoomSpeed, smoothTime);

        cam.orthographicSize = smoothZoomSize;
    }
}
