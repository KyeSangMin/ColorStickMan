using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSystem : MonoBehaviour
{


    public GameObject[] ButtonStatic;
    int Indexnum;
    public int MaxIndex = 10;

    GameObject TempObject;
    // Start is called before the first frame update
    void Start()
    { 
        Indexnum = 0;
        GameObject[] ButtonStatic = new GameObject[MaxIndex];
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void PushIndex(GameObject gameobject)
    {
        ButtonStatic[Indexnum] = gameobject;
        Indexnum++;
    }

    public void PopIndex()
    {
        ButtonStatic[Indexnum] = null;
        Indexnum--;
    }

    public void ResetIndex()
    {
        for (int i = 0; i < Indexnum; Indexnum-- )
        {
            ButtonStatic[Indexnum] = null;
        }
        Indexnum = 0;
    }

    public bool CheckIndex(GameObject gameObject)
    {
        Debug.Log(gameObject.name);
        TempObject = gameObject;
        if (Indexnum == 0 && ButtonStatic[Indexnum] == TempObject)
            return true;
        else if (ButtonStatic[Indexnum-1] == TempObject)
        {
            return true;
        }

        else
        {
            return false;
        }
    }


}
